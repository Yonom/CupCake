using System;
using System.Collections.Generic;
using System.Linq;

namespace CupCake.Host
{
    internal static class EnumHelper
    {
        internal static IEnumerable<T> GetIndividualValues<T>(this Enum myEnum) where T : struct
        {
            return myEnum
                .ToString()
                .Split(new[] {','})
                .Select(x => (T)Enum.Parse(typeof(T), x.Trim()))
                .ToUniqueFlagEnumValues();
        }

        internal static bool IsPowerOfTwo(this int value)
        {
            return (value & (value - 1)) == 0;
        }

        internal static IEnumerable<T> ToUniqueFlagEnumValues<T>(this IEnumerable<T> flagsEnumValues) where T : struct
        {
            foreach (T item in flagsEnumValues)
            {
                int intValue = Convert.ToInt32(item);
                //if our int is a power of two, it's a unique value of the flags enum
                if (intValue.IsPowerOfTwo())
                {
                    yield return item;
                }
                    //otherwise its a combination of several unique values and we need to break it down further
                else
                {
                    //the enum value output as binary string representation
                    string fullBinaryString = Convert.ToString(intValue, 2);
                    //an empty template with all 0's that is the length of our binary string
                    char[] individualBitTemplate = new string('0', fullBinaryString.Length).ToCharArray();

                    IEnumerable<T> individualFlagsEnumValues = fullBinaryString
                        .Select((character, index) =>
                        {
                            //project each individual bit into its own binary string with 0's in every position
                            //other than the index of the individual bit
                            //Example: binary string 1111
                            //produces 4 individual binary strings
                            //0001
                            //0010
                            //0100
                            //1000
                            var template = (char[])individualBitTemplate.Clone();
                            template[index] = character;
                            return new string(template);
                        })
                        .Where(individualBitBinaryString =>
                        {
                            //filter the results to exclude any binary strings that are all 0's
                            return individualBitBinaryString.Any(character => character != '0');
                        })
                        .Select(individualBitBinaryString =>
                        {
                            //cast the individual binary strings back to their int value, and then into the enum value
                            int intValueOfIndividualBit = Convert.ToInt32(individualBitBinaryString, 2);
                            return (T)Enum.ToObject(typeof(T), intValueOfIndividualBit);
                        });

                    foreach (T value in individualFlagsEnumValues)
                    {
                        yield return value;
                    }
                }
            }
        }
    }
}