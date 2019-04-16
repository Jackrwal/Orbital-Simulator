using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JWOrbitalSimulatorPortable.Model
{
    static public class SystemFileParser
    {
        // Reader and writer used for File IO
        private static StreamReader FileReader;
        private static StreamWriter FileWriter;

        // The file path that the application is executing from.
        public static string ExecutingDomainFilePath => AppDomain.CurrentDomain.BaseDirectory; 

        /// <summary>
        /// Get the file path's of all text files in the executing directory created by the file parser.
        /// </summary>
        /// <returns></returns>
        static public List<string> GetReadableSaveFiles()
        {

            // Get the directory of the application
            DirectoryInfo ExecutingDirectory = new DirectoryInfo(ExecutingDomainFilePath);

            // Find any files in this folder matching the save file name format (Save File Name)OSSaveFile.txt
            FileInfo[] SaveFilesInExecutingDirectory = ExecutingDirectory.GetFiles("*OSSaveFile.txt");

            // return the List of save files full file paths as a list 
            return SaveFilesInExecutingDirectory.Select((SaveFile) => SaveFile.FullName).ToList();
        }

        /// <summary>
        /// Read an InterstellarSystem from a given file path
        /// </summary>
        static public InterstellaSystem ReadSystemFile(string SaveFileString)
        {
            List<string> ReadLines = new List<string>();

            // Attempt to read the save file line by line
            try
            {
               
                FileReader = new StreamReader(SaveFileString);

                // the next line of the given file until the reader reaches the end of the file.
                while (!FileReader.EndOfStream) ReadLines.Add(FileReader.ReadLine());
            }
            finally
            {
                // Close the file reader regardless of whether the read operation is succesfull
                FileReader.Close();
            }

            //return the file lines as an interstellar system.
            return parseSaveFileToSystem(ReadLines);
        }

        /// <summary>
        /// Get the Save Name and Date of a system file with out reading and parsing the full system
        /// </summary>
        /// <param name="SaveFileString"></param>
        /// <returns></returns>
        static public string[] PreReadSystemFile(string SaveFileString)
        {
            List<string> ReadLines = new List<string>();

            // Attempt to read the save file line by line
            try
            {
                
                FileReader = new StreamReader(SaveFileString);

                // the next line of the given file until the reader reaches the end of the file.
                while (!FileReader.EndOfStream) ReadLines.Add(FileReader.ReadLine());
            }
            finally
            {
                // Close the file reader regardless of whether the read operation is succesfull
                FileReader.Close();
            }

            // Tracks the current line of the text file
            int LineIndex = 0;

            // start the current save line at the first line of the text file
            string SaveLine = ReadLines[LineIndex];

            // Set SystemName and SaveTime to empty until they are found.
            string SystemName = string.Empty;
            string SaveTime = string.Empty;

            // While either SystemName or SaveTime have not been found  
            while (SystemName == string.Empty || SaveTime == string.Empty)
            {
                // if the line is a comment line, beggining '#', move to the next line.
                if (SaveLine[0] == '#') SaveLine = ReadLines[LineIndex++];

                // Check if the saveline key matches the Tag for the System Name Property
                if (SaveLine.Length >= _DataReadTypes[0].Tag.Length && SaveLine.Substring(0, _DataReadTypes[0].Tag.Length) == _DataReadTypes[0].Tag)
                    // Get the data string seperate from the tag or any file syntax and assign it to system Name.
                    SystemName = ExtractLineData(SaveLine, _DataReadTypes[0]).DataString;

                // Check if the saveline key matches the Tag for the SaveTime Property
                if (SaveLine.Length >= _DataReadTypes[9].Tag.Length && SaveLine.Substring(0, _DataReadTypes[9].Tag.Length) == _DataReadTypes[9].Tag)
                    SaveTime = ExtractLineData(SaveLine, _DataReadTypes[9]).DataString;

                // Incriment Line Index and move SaveLine to the next line.
                SaveLine = ReadLines[LineIndex++];
            }

            // Return a string array of the system's name and save time.
            return new string[2] { SystemName, SaveTime };
        }

        /// <summary>
        /// Delete a given system file.
        /// </summary>
        static public void DeleteSaveFile(string fileString)
        {
            // If the file was not generated by the file parser do not delete it.
            if (!fileString.Contains("OSSaveFile.txt")) return;

            try
            {
                File.Delete(fileString);
            }
            catch
            {
                // If the file does not exist return as we cannot delete it
                return;
            }
        }

        /// <summary>
        /// Over-write an existing save file with
        /// </summary>
        static public bool OverWriteSystemFile(InterstellaSystem systemToSave, string fileStringToOverWrite)
        {
            // Get the InterstellarSystem object as a list of strings in a text format.
            List<string> SaveLines = parseSystemToSaveFile(systemToSave);

            // Attempt to overwrite the file of the given path.
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

        /// <summary>
        /// Save an InterstellarSystem object to a new save file.
        /// </summary>
        static public bool SaveNewSystemFile(InterstellaSystem systemToSave)
        {
            // Get the InterstellarSystem object as a list of strings in a text format.
            List<string> SaveLines = parseSystemToSaveFile(systemToSave);

            try
            {
                // Attempt to open a new save file in the executing directory and write to the system to it.
                FileWriter = new StreamWriter(ExecutingDomainFilePath + systemToSave.SystemSaveName + "OSSaveFile.txt");
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

        /// <summary>
        /// Parse an InterstellarSystem Object into a text format.
        /// </summary>
        static private List<string> parseSystemToSaveFile(InterstellaSystem system)
        {
            List<string> TextSaveFormat = new List<string>();

            // add file infomation header
            TextSaveFormat.Add($"#{system.SystemSaveName} Orbital Simulator Save File");
            TextSaveFormat.Add($"SaveTime: { DateTime.Now },");

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
                TextSaveFormat.Add($"       Acceleration:  {{{interstellaObject.Acceleration.X}, {interstellaObject.Acceleration.Y} }},");
                TextSaveFormat.Add($"       Mass: {interstellaObject.Mass},");
                TextSaveFormat.Add($"       Radius: {interstellaObject.Radius},");
                TextSaveFormat.Add("    },");
            }

            TextSaveFormat.Add($"]");
            return TextSaveFormat;
        }

        /// <summary>
        /// The key:value pairs of each saved property of an interstellar system.
        /// </summary>
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
            new Data{ Tag = "Radius:", DesiredType = typeof(double) },

            new Data{ Tag = "SaveTime:", DesiredType = typeof(string) }
        };

        /// <summary>
        /// Parse a System File into a <see cref="InterstellaSystem"/>
        /// </summary>
        static private InterstellaSystem parseSaveFileToSystem(List<string> saveFileLines)
        {
            // the current save line.
            string SaveLine;

            List<Data> IntermediateDataFormatSystem = new List<Data>();

            // loop through each line of the save file.
            for (int i = 0; i < saveFileLines.Count; i++)
            {
                SaveLine = saveFileLines[i];

                // Ignore line comments marked by a '#'
                if (SaveLine[0] == '#') continue;

                // Get the Data container for this line
                Data LineDataType = GetLineDataType(SaveLine);

                // If the Read Data Type is a collection read the collection
                if (LineDataType.Tag != null && LineDataType.DesiredType.GetInterface(nameof(ICollection<InterstellaObject>)) != null)
                {
                    IntermediateDataFormatSystem.Add(ReadLinesCollection(saveFileLines, LineDataType, ref i));

                }
                // If this is a simple data line add it's data string to the intermediate format.
                else if(LineDataType.Tag != null) IntermediateDataFormatSystem.Add(ExtractLineData(SaveLine, LineDataType));

            }

            try
            {
                // Attempt to pass the intermediate from into an object.
                return ParseIntermediateFormToSystem(IntermediateDataFormatSystem);
            }
            catch (Exception)
            {
                // if unable to parse the file to an object, if the file is corrupted or tampered with. Return a blank object.
                return new InterstellaSystem();
            }
        }

        /// <summary>
        /// Converts a complete List of Data Objects and returns an InterstellarSystem Object
        /// </summary>
        private static InterstellaSystem ParseIntermediateFormToSystem(List<Data> intermediateDataFormatSystem)
        {
            InterstellaSystem DataAsSystemType = new InterstellaSystem();

            // loop through each data object
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

                            // Get Radius
                            else if (ObjectProperty.Tag == _DataReadTypes[8].Tag)
                                ObjectParams.Radius = (double)Convert.ChangeType(ObjectProperty.DataString, ObjectProperty.DesiredType);

                        }
                        // Use the retrieved data to construct an Interstellar Object
                        Objects.Add(new InterstellaObject(ObjectParams));
                    }
                    // Add these objects to the system
                    DataAsSystemType.InterstellaObjects = Objects;
                }

            }
            return DataAsSystemType;
        }

        /// <summary>
        /// Attempts to return the enum type from it's string
        /// </summary>
        private static InterstellaObjectType ConvertDataStringToTypeEnum(string dataString)
        {
            InterstellaObjectType Type;

            // if cast is sucessfull return the enum value.
            if (Enum.TryParse(dataString, out Type))
            {
                return Type;
            }
            throw new InvalidCastException();
        }

        /// <summary>
        /// Attempts to cast a vector from intermediate form.
        /// </summary>
        private static Vector CastDataToVector(List<Data> dataStrings)
        {
            // Convert each data string (X and Y) of the object to a double and return the vector.
            return new Vector
            (
                (double)Convert.ChangeType(dataStrings[0].DataString, dataStrings[0].DesiredType),
                (double)Convert.ChangeType(dataStrings[1].DataString, dataStrings[1].DesiredType)
            );
        }

        /// <summary>
        /// Gets the Data Type of a Tag:Value string
        /// </summary>
        private static Data GetLineDataType(string saveLine)
        {
            Data ReturnData = new Data();

            //Ommit ' 's from the string 
            saveLine = saveLine.Replace(" ", string.Empty);

            // check the line against each data type
            foreach (var DataType in _DataReadTypes)
            {
                // Compare the line's data tag to the current DataType
                if (saveLine.Length >= DataType.Tag.Length && saveLine.Substring(0, DataType.Tag.Length) == DataType.Tag)
                {
                    // If this data type's tag matches the line return this data type.
                    return DataType;
                }
            }

            // Unknown data tag
            throw new InvalidOperationException();
        }   

        /// <summary>
        /// Returns the 
        /// </summary>
        /// <param name="saveFileLines">The Save File strings</param>
        /// <param name="dataType">data object of the collection</param>
        /// <param name="lineIndex">the current read index for the file</param>
        static private Data ReadLinesCollection(List<string> saveFileLines, Data dataType, ref int lineIndex)
        {
            List<Data> DataCollection = new List<Data>();

            // Move from the collection Tag, to the collection
            lineIndex++;
            string saveLine = saveFileLines[lineIndex];
                
            // If this is the beggining of a collection..
            if (saveLine[0] == '[')
            {
                // Read until the closing tag of the collection.
                while (saveLine[0] != ']')
                {
                    // Move onto the next line, the collection.
                    lineIndex++;
                    saveLine = saveFileLines[lineIndex];

                    // ommit ' ' from the string..
                    // if the next character is a '{' it is the opening of a data object with multiple properties stored about it.
                    if (saveLine.Replace(" ", string.Empty)[0] == '{')
                    {
                        // This means i must read until '}' and store all properties about this object.
                        // Each of these will be stored as a Data object and added to the DataCollection.
                        DataCollection.Add(ReadComplexObject(saveFileLines, ref lineIndex));
                    }

                }
            }

            // Assign the data objects, each representing an object, to the Data Object for the collection
            dataType.DataStrings = DataCollection;
            return dataType;
        }

        /// <summary>
        /// Read a anonamous data object with multiple properties
        /// </summary>
        private static Data ReadComplexObject(List<string> saveFileLines, ref int lineIndex)
        {
            // Store Object Properties (there tags, types and values)
            List<Data> DataObjectProperties = new List<Data>();

            // skip the opening '{' and move onto the collection.
            lineIndex++;

            // Ommit ' ''s used for indenting from the save line
            string saveLine = saveFileLines[lineIndex].Replace(" ", string.Empty); 

            while (saveLine[0] != '}')
            {

                // Get the type of the property
                Data saveLineDataType = GetLineDataType(saveLine);

                // Extract the string for the value from the tag:value line.
                DataObjectProperties.Add(ExtractLineData(saveLine, saveLineDataType));

                // Move to the next line.
                lineIndex++;
                saveLine = saveFileLines[lineIndex].Replace(" ", string.Empty); ;
            }
            // Return a Data Object containing the properties
            return new Data { DataStrings = DataObjectProperties, DesiredType=typeof(InterstellaObject) };
        }

        /// <summary>
        /// Extract the value string from a Tag:Value line
        /// </summary>
        /// <returns></returns>
        static private Data ExtractLineData(string dataLine, Data DataType)
        {
            // ommit ' 's used for spaces from the line used for indents
            string LineWithOutBlanks = dataLine.Replace(" ", string.Empty);

            // Get the Value string of the line.
            string dataString = LineWithOutBlanks.Substring(DataType.Tag.Length, LineWithOutBlanks.Length - DataType.Tag.Length);

            int dataIndex = 0;
            char Character = dataString[dataIndex];

            // Loop through each character of the line untill the ending ',' is reached
            while (Character != ',')
            {
                Character = dataString[dataIndex++];

                // Search for complex data types in a line such as vectors  'Position:      {0, 0 },'
                if (Character == '{')
                {
                    Character = dataString[dataIndex++];
                    
                    List<Data> DataPieces = new List<Data>();
                    string DataPiece = "";

                    // If a complex data object is found loop through the character until the end of the object.
                    while (Character != '}')
                    {
                        // Extract each data piece ommiting special characters.
                        DataPiece = "";
                        while (Character != ',' && Character != '}')
                        {
                            if(Character != ',' && Character != '}')
                                DataPiece += Character;

                            Character = dataString[dataIndex++];
                        }

                        DataPieces.Add(new Data { DataString = DataPiece, DesiredType = typeof(double) });

                        // When the ending '}' is reached move onto the next character ( special case stops an infinity loop )
                        if(Character != '}') Character = dataString[dataIndex++];

                    }

                    // Assign each data piece to a data object in the complex data object.
                    DataType.DataStrings = DataPieces;
                }
                // Prevents an infinity loop
                if (Character != ' ' && Character != ',') DataType.DataString += Character;
            }

            return DataType;
        }

        /// <summary>
        /// Private Embedded Struct to store each serialized data piece in an intermediate form
        /// before casting back to the desired object.
        /// </summary>
        private struct Data
        {
            // The key underwhich the data piece is stored
            public string Tag { get; set; }

            // The serialised value as a string
            public string DataString { get; set; }

            // For non-simple data pieces multiple data objects are stored as members of that object.
            // This may be members of a collection. Or the serialzied properties of an object.
            public List<Data> DataStrings { get; set; }

            // The type of object that the parser will attempt to pass the serialzied value back to.
            // This is based on the Tag of the value.
            public Type DesiredType { get; set; }
        }
    }
}