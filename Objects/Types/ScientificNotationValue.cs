using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrbitalSimulator_Objects
{
    //Turns out all of this functionality was already programmed into the double type, but oh well..

    // Double returning infinity for very large numbers could be problamatic depending on whether it does calculations with infinity or just throws an error.
    // Be carefull of this going foward.

    // Class probably dosnt work with negative exponents with out some modifications, not sure yet.
    // I will make this suitable for negative exponents if i need to.

    public class ScientificNotationValue
    {
        double _Mantissa;
        int _Exponent;

        public double Mantissa { get => _Mantissa; set => _Mantissa = value; }
        public int Exponent { get => _Exponent; set => _Exponent = value; }

        public override string ToString()
        {
            return $"{_Mantissa}E+{_Exponent}";
        }

        public ScientificNotationValue(double mantissa, int exponent)
        {
            _Mantissa = mantissa;
            _Exponent = exponent;

            if (mantissa < 1 || mantissa > 10)
            {
                ScientificNotationValue Value = normalise(this);
                _Mantissa = Value.Mantissa;
                _Exponent = Value.Exponent;
            }
        }

        public ScientificNotationValue(double value)
        {
            ScientificNotationValue notationValue = normalise(new ScientificNotationValue(value, 0));
            _Mantissa = notationValue.Mantissa;
            _Exponent = notationValue.Exponent;
        }

        public static ScientificNotationValue operator +(ScientificNotationValue V1, ScientificNotationValue V2)
        {
            equateExponents(ref V1, ref V2);
            return normalise(new ScientificNotationValue(V1.Mantissa + V2.Mantissa, V1.Exponent));
        }

        public static ScientificNotationValue operator -(ScientificNotationValue V1, ScientificNotationValue V2)
        {
            equateExponents(ref V1, ref V2);
            return normalise(new ScientificNotationValue(V1.Mantissa - V2.Mantissa, V1.Exponent));
        }

        public static ScientificNotationValue operator *(ScientificNotationValue V1, ScientificNotationValue V2)
        {
            return normalise(new ScientificNotationValue(V1.Mantissa * V2.Mantissa, V1.Exponent + V2.Exponent));
        }

        public static ScientificNotationValue operator /(ScientificNotationValue V1, ScientificNotationValue V2)
        {
            return normalise(new ScientificNotationValue(V1.Mantissa / V2.Mantissa, V1.Exponent - V2.Exponent));
        }

        public double ToDouble()
        {
            // ## Can probably write a converter / cast for this ?
            return _Mantissa * Math.Pow(10, _Exponent);
        }

        

        /// <summary>
        /// Normalises a Scientific Notation Value by changing the Exponent so that the mantissa is between 1 and 10
        /// </summary>
        /// <param name="value">A unnormalised ScientficNotationValue </param>
        /// <returns> a normalised ScientificNotationValue </returns>
        private static ScientificNotationValue normalise(ScientificNotationValue value)
        {
            if (value.Mantissa > 1 && value.Mantissa < 10)
            {
                return value;
            }

            while ( value.Mantissa < 1 || value.Mantissa >= 10)
            {
                if (value.Mantissa >= 10)
                {
                    value.Mantissa /= 10;
                    value.Exponent++;
                }
                else
                {
                    value.Mantissa *= 10;
                    value.Exponent--;
                }
            }
            return value;
        }

        /// <summary>
        /// References two ScientificNotationValues and modifies the smaller value to give it the same exponent as the larger value
        /// To facilitate Addition and Subtraction operators
        /// </summary>
        /// <param name="V1"> refference to the first Scientific Notation Value </param>
        /// <param name="V2"> refference to the second Scientific Notation Value </param>
        private static void equateExponents(ref ScientificNotationValue V1, ref ScientificNotationValue V2)
        {
            if (V1.Exponent == V2.Exponent)
            {
                return;
            }

            // Bring the smaller Exponent Equal to the larger one
            // Multiply the Mnatissa of the smaller number by 10 to the power of the difference and then set the exponent equal
            if (V1.Exponent > V2.Exponent)
            {
                int Difference = V2.Exponent - V1.Exponent;
                V2.Mantissa *= Math.Pow(10, Difference);
                V2.Exponent = V1.Exponent;
            }
            else
            {
                int Difference = V1.Exponent - V2.Exponent;
                V1.Mantissa *= Math.Pow(10, Difference);
                V1.Exponent = V2.Exponent;
            }
        }
    }
}
