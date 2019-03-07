using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JWOrbitalSimulatorPortable.Model
{
    static public class SystemFileParser
    {
        private static StreamReader FileReader;
        private static StreamWriter FileWriter;

        private static string ExecutingDomainFilePath { get { return AppDomain.CurrentDomain.BaseDirectory; } }

        static public List<string> GetReadableSaveFiles()
        {
            // Get the directory of the application
            DirectoryInfo ExecutingDirectory = new DirectoryInfo(ExecutingDomainFilePath);

            // Find any files in this folder matching the save file name format (Save File Name)OSSaveFile.txt
            FileInfo[] SaveFilesInExecutingDirectory = ExecutingDirectory.GetFiles("*OSSaveFile.txt");

            // return the List of save files full file paths as a list 
            return SaveFilesInExecutingDirectory.Select((SaveFile) => SaveFile.FullName).ToList();
        }

        static public InterstellaSystem ReadSystemFile(string SaveFileString)
        {
            List<string> ReadLines = new List<string>();

            try
            {
                // Attempt to read the save file line by line
                FileReader = new StreamReader(SaveFileString);
                while (!FileReader.EndOfStream) ReadLines.Add(FileReader.ReadLine());
            }
            finally
            {
                // Close the file reader regardless of whether the read operation is succesfull
                FileReader.Close();
            }

            return parseSaveFileToSystem(ReadLines);
        }

        static public bool OverWriteSystemFile(InterstellaSystem systemToSave, string fileStringToOverWrite)
        {
            List<string> SaveLines = parseSystemToSaveFile(systemToSave);

            try
            {
                // Open the stream writer to the file path to over write, set not to append (to overwrite)
                FileWriter = new StreamWriter(fileStringToOverWrite, false);
                foreach (var Line in SaveLines)
                {
                    FileWriter.WriteLine(Line);
                }
            }
            catch
            {
                // Return false if the save operation fails
                return false;
            }
            finally
            {
                // Close the file writer regardless of if the save operation is successfull
                FileWriter.Close();
            }

            // return true if the save operation is sucessfull
            return true;
        }

        static public bool SaveNewSystemFile(InterstellaSystem systemToSave, string newFileName)
        {
            List<string> SaveLines = parseSystemToSaveFile(systemToSave);

            try
            {
                // Attempt to save the system new save file to the executing directory and write too it.
                FileWriter = new StreamWriter(ExecutingDomainFilePath + newFileName + "OSSaveFile.txt");
                foreach (var Line in SaveLines)
                {
                    FileWriter.WriteLine(Line);
                }
            }
            catch
            {
                // return false if the save operation is unsucesfull
                return false;
            }
            finally
            {
                // Close the file writer regardless of if the save operation is successfull

                FileWriter.Close();
            }

            // return true if the save operation is sucessfull
            return true;
        }

        static private List<string> parseSystemToSaveFile(InterstellaSystem system)
        {
            List<string> TextSaveFormat = new List<string>();

            // add file infomation header
            TextSaveFormat.Add($"#{system.SystemSaveName} Orbital Simulator Save File");
            TextSaveFormat.Add($"SaveTime: { DateTime.Now }");

            // Add System
            TextSaveFormat.Add($"SystemName: {system.SystemSaveName},");
            TextSaveFormat.Add($"CollissionMassRetentionPercentage: {system.CollissionMassRetentionPercentage},");
            TextSaveFormat.Add($"Objects:");

            // Add Objects
            TextSaveFormat.Add($"[");

            foreach (InterstellaObject interstellaObject in system.InterstellaObjects)
            {
                TextSaveFormat.Add("    {");
                TextSaveFormat.Add($"       Type: {interstellaObject.Type.ToString()},");
                TextSaveFormat.Add($"       Position:      {{{interstellaObject.Position.X}, {interstellaObject.Position.Y} }},");
                TextSaveFormat.Add($"       Velocity:      {{{interstellaObject.Velocity.X}, {interstellaObject.Velocity.Y} }},");
                TextSaveFormat.Add($"       Acceleration:  {{{interstellaObject.Accelleration.X}, {interstellaObject.Accelleration.Y} }},");
                TextSaveFormat.Add($"       Mass: {interstellaObject.Mass},");
                TextSaveFormat.Add($"       Radius: {interstellaObject.Radius},");
                TextSaveFormat.Add("    },");
            }

            TextSaveFormat.Add($"]");
            return TextSaveFormat;
        }

        private static List<Data> _DataReadTypes = new List<Data>
        {
            new Data{ Tag = "SystemName:", DesiredType = typeof(string) },
            new Data{ Tag = "CollissionMassRetentionPercentage:", DesiredType = typeof(Double) },

            new Data{ Tag = "Objects:", DesiredType = typeof(List<InterstellaObject>) },

            new Data{ Tag = "Type:", DesiredType = typeof(InterstellaObjectType) },
            new Data{ Tag = "Position:", DesiredType = typeof(Vector) },
            new Data{ Tag = "Velocity:", DesiredType = typeof(Vector) },
            new Data{ Tag = "Acceleration:", DesiredType = typeof(Vector) },
            new Data{ Tag = "Mass:", DesiredType = typeof(double) },
            new Data{ Tag = "Radius:", DesiredType = typeof(double) }
        };

        /// <summary>
        /// Parse a System File into a <see cref="InterstellaSystem"/>
        /// </summary>
        /// <param name="saveFileLines"></param>
        /// <returns></returns>
        static private InterstellaSystem parseSaveFileToSystem(List<string> saveFileLines)
        {
            string SaveLine;

            List<Data> IntermediateDataFormatSystem = new List<Data>();

            for (int i = 0; i < saveFileLines.Count; i++)
            {
                SaveLine = saveFileLines[i];

                // Ignore commments
                if (SaveLine[0] == '#') continue;

                // Get the Data container for this line
                Data LineDataType = GetLineDataType(SaveLine);

                // If the Read Data Type is a collection proccede to read collection
                if (LineDataType.Tag != null && LineDataType.DesiredType.GetInterface(nameof(ICollection<InterstellaObject>)) != null)
                {
                    IntermediateDataFormatSystem.Add(ReadLinesCollection(saveFileLines, LineDataType, ref i));

                }
                // If this is a simple data line add it to the intermediate format
                else if(LineDataType.Tag != null) IntermediateDataFormatSystem.Add(ExtractLineData(SaveLine, LineDataType));

            }

            return ParseIntermediateFormToSystem(IntermediateDataFormatSystem);
        }

        // ~~ This function is definitly getting too large
        private static InterstellaSystem ParseIntermediateFormToSystem(List<Data> intermediateDataFormatSystem)
        {
            InterstellaSystem DataAsSystemType = new InterstellaSystem();

            foreach (var DataItem in intermediateDataFormatSystem)
            {
                // Check For System Name
                if (DataItem.Tag == _DataReadTypes[0].Tag)
                    DataAsSystemType.SystemSaveName = DataItem.DataString;

                // Check For System Mass Retention
                else if (DataItem.Tag == _DataReadTypes[1].Tag)
                    DataAsSystemType.CollissionMassRetentionPercentage = (double)Convert.ChangeType(DataItem.DataString, DataItem.DesiredType);

                // Check for objects collections
                else if (DataItem.Tag == _DataReadTypes[2].Tag)
                {
                    List<InterstellaObject> Objects = new List<InterstellaObject>();

                    // Cast each data string too a interstella Object
                    foreach (var DataObject in DataItem.DataStrings)
                    {
                        InterstellaObjectParams ObjectParams = new InterstellaObjectParams();

                        // Get Properties
                        foreach (var ObjectProperty in DataObject.DataStrings)
                        {
                            // Get Type
                            if (ObjectProperty.Tag == _DataReadTypes[3].Tag)
                                ObjectParams.Type = ConvertDataStringToTypeEnum(ObjectProperty.DataString);

                            // Get Position
                            else if (ObjectProperty.Tag == _DataReadTypes[4].Tag)
                                ObjectParams.Position = CastDataToVector(ObjectProperty.DataStrings);

                            // Get Velocity
                            else if (ObjectProperty.Tag == _DataReadTypes[5].Tag)
                                ObjectParams.Velocity = CastDataToVector(ObjectProperty.DataStrings);

                            // Get Acceleration
                            else if (ObjectProperty.Tag == _DataReadTypes[6].Tag)
                                ObjectParams.Acceleration = CastDataToVector(ObjectProperty.DataStrings);

                            // Get Mass
                            else if (ObjectProperty.Tag == _DataReadTypes[7].Tag)
                                ObjectParams.Mass = (double)Convert.ChangeType(ObjectProperty.DataString, ObjectProperty.DesiredType); 
                            // !! For some reason desired type of mass got set to a collection

                            // Get Radius
                            else if (ObjectProperty.Tag == _DataReadTypes[8].Tag)
                                ObjectParams.Radius = (double)Convert.ChangeType(ObjectProperty.DataString, ObjectProperty.DesiredType);

                        }

                        Objects.Add(new InterstellaObject(ObjectParams));
                    }

                    DataAsSystemType.InterstellaObjects = Objects;
                }

            }
            // Radius and dencity returned as 0
            // All Velocity Returned as 0,0
            return DataAsSystemType;
        }

        private static InterstellaObjectType ConvertDataStringToTypeEnum(string dataString)
        {
            InterstellaObjectType Type;

            if (Enum.TryParse(dataString, out Type))
            {
                return Type;
            }
            throw new InvalidCastException();
        }

        private static Vector CastDataToVector(List<Data> dataStrings)
        {
            return new Vector
            (
                (double)Convert.ChangeType(dataStrings[0].DataString, dataStrings[0].DesiredType),
                (double)Convert.ChangeType(dataStrings[1].DataString, dataStrings[1].DesiredType)
            );
        }

        private static Data GetLineDataType(string saveLine)
        {
            Data ReturnData = new Data();

            //Ommit ' 's from the string 
            saveLine = saveLine.Replace(" ", string.Empty);

            foreach (var DataType in _DataReadTypes)
            {
                // Must check SaveLine Length first or sub string creates an index out of range when data tag length is greater than the save line length
                // This also avoids taking the sub string if the tag and saveline characters clearly wont be the same (as they are too different in length)
                if (saveLine.Length >= DataType.Tag.Length && saveLine.Substring(0, DataType.Tag.Length) == DataType.Tag)
                {
                    ReturnData = DataType;
                }
            }

            return ReturnData;
        }

        static private Data ReadLinesCollection(List<string> saveFileLines, Data dataType, ref int lineIndex)
        {
            List<Data> DataCollection = new List<Data>();

            // Move to the collection
            lineIndex++;
            string saveLine = saveFileLines[lineIndex];

            if (saveLine[0] == '[')
            {
                while (saveLine[0] != ']')
                {
                    lineIndex++;
                    saveLine = saveFileLines[lineIndex];


                    // if the next character is a '{' it is the opening of a data object with multiple properties stored about it.
                    // This means i must read untill '}' and store all properties about this object.
                    // Each of these will be stored as a Data, which will contain multiple DataStrings, which represent all the properties of the object
                    if (saveLine.Replace(" ", string.Empty)[0] == '{')
                    {
                        DataCollection.Add(ReadComplexObject(saveFileLines, ref lineIndex));
                    }
                    

                }
            }

            dataType.DataStrings = DataCollection;
            return dataType;
        }

        // ~~ This is almost identical to ReadlinesCollection, affter all a collection is just a complex data object (This is starting to show why the main reading function should be recursive)
        private static Data ReadComplexObject(List<string> saveFileLines, ref int lineIndex)
        {
            // Store Object Properties (there tags, types and values)
            List<Data> DataObjectProperties = new List<Data>();

            // Move to the collection
            lineIndex++;
            string saveLine = saveFileLines[lineIndex].Replace(" ", string.Empty); // ~~ I Use this line so much that maybe i should just make a get next read line function


            while (saveLine[0] != '}')
            {
                if(saveLine[0] != '}')
                {
                    // Get the type of the property
                    Data saveLineDataType = GetLineDataType(saveLine);

                    DataObjectProperties.Add(ExtractLineData(saveLine, saveLineDataType));

                }

                lineIndex++;
                saveLine = saveFileLines[lineIndex].Replace(" ", string.Empty); ;
            }
            

            return new Data { DataStrings = DataObjectProperties };
        }

        /// <summary>
        /// Extract the piece of data between the data tag and the comma denoting the start of the next data label
        /// </summary>
        /// <param name="dataLine"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        static private Data ExtractLineData(string dataLine, Data DataType)
        {
            string LineWithOutBlanks = dataLine.Replace(" ", string.Empty);

            string dataString = LineWithOutBlanks.Substring(DataType.Tag.Length, LineWithOutBlanks.Length - DataType.Tag.Length);

            int dataIndex = 0;
            char Character = dataString[dataIndex];


            // ~~ Opertmise this to have less nested loops later, maybe extract the ',' searcher as a function and use it recusivly
            while (Character != ',')
            {
                Character = dataString[dataIndex++];
              
                // Read anonamous complex data object
                if (Character == '{')
                {
                    Character = dataString[dataIndex++];

                    List<Data> DataPieces = new List<Data>();
                    string DataPiece = "";

                    while (Character != '}')
                    {
                        DataPiece = "";
                        while (Character != ',' && Character != '}')
                        {
                            if(Character != ',' && Character != '}')
                                DataPiece += Character;

                            Character = dataString[dataIndex++];
                        }


                        // ~~ Make Data Piece a Data type instead of string and just append too the tag
                        DataPieces.Add(new Data { DataString = DataPiece, DesiredType = typeof(double) });

                        if(Character != '}') Character = dataString[dataIndex++];

                    }

                    DataType.DataStrings = DataPieces;
                    DataType.DataString = null;
                }

                if (Character != ' ' && Character != ',') DataType.DataString += Character;
            }

            

            return DataType;
        }

        private struct Data
        {
            public string Tag { get; set; }

            // For simple data items which only have a value to store
            public string DataString { get; set; }

            // For complex data items that have mutliple data items stored, such as the members of a collection, or properties of a complex object
            public List<Data> DataStrings { get; set; }

            public Type DesiredType { get; set; }
        }
    }
}
