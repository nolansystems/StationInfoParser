using StationInfo.Parser.Library;
using System;
using System.Collections.Generic;

namespace StationInfo.Parser
{
    /// <summary>
    /// Takes a string of one or more station data lines and parses the values into individual
    /// station objects
    /// </summary>
    public class StationParser
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public StationParser()
        {
        }

        #region Data Parsing

        /// <summary>
        /// Extracts station objects out of the raw text
        /// </summary>
        /// <param name="rawText">
        /// Text formatted with values falling within a specific index range where each station is
        /// separated by a new line character
        /// </param>
        /// <returns>Array of available weather stations</returns>
        public List<Library.Station> ParseText(string rawText)
        {
            if (rawText == null)
            {
                throw new ArgumentException(string.Format("{0} should not be null or empty",
                    nameof(rawText)));
            }

            var parsedData = new List<Library.Station>();
            var stateCountry = string.Empty;

            foreach (var line in SplitTextOnLines(rawText))
            {
                var trimmedLine = line.TrimEnd();
                // Comment lines start with an exclamation point
                if (trimmedLine.StartsWith("!")
                    || trimmedLine.Length == 78)
                {
                    continue;
                }
                // Station lines are 83 characters long for US and 80 characters for all other countries
                if (line.Length == 83)
                {
                    var station = ExtractStationFromLine(line);
                    if (station == null)
                    {
                        continue;
                    }
                    station.StateCountryName = stateCountry;
                    parsedData.Add(station);
                }
                // Station/Province/Country line
                else if (trimmedLine.Length > 0)
                {
                    int spaceIdx = trimmedLine.LastIndexOf(' ');
                    if (spaceIdx > 0)
                    {
                        stateCountry = trimmedLine.Substring(0, spaceIdx).Trim();
                    }
                    else
                    {
                        stateCountry = trimmedLine;
                    }
                }
            }

            return parsedData;
        }

        /// <summary>
        /// Parses out all values from the line and creates a
        /// <code>
        /// Station
        /// </code>
        /// </summary>
        /// <param name="line">Single line of station data</param>
        /// <returns></returns>
        public Library.Station ExtractStationFromLine(string line)
        {
            int temp;
            var station = new Library.Station();
            try
            {
                // All of the values are index based
                station.StateProvinceAbbrev = line.Substring(0, 2);
                station.StationName = line.Substring(3, 16).Trim();
                station.ICAO = line.Substring(20, 4);
                if (string.IsNullOrWhiteSpace(station.ICAO))
                {
                    return null;
                }
                station.IATA = line.Substring(26, 3);

                if (int.TryParse(line.Substring(32, 5), out temp))
                {
                    station.SYNOP = temp;
                }

                station.Latitude = line.Substring(39, 6);
                station.Longitude = line.Substring(47, 7);

                if (int.TryParse(line.Substring(56, 6), out temp))
                {
                    station.Elevation = temp;
                }

                // The following values are all converted to enumerations as they should be a
                // standard set of values for each field
                station.METAR = ParseValueForMETAR(line.Substring(62, 1));
                station.RADAR = ParseValueForRADAR(line.Substring(65, 1));
                station.AviationFlag =
                    ParseValueForAviationFlag(line.Substring(68, 1));
                station.UpperAir =
                    ParseValueForUpperAir(line.Substring(71, 1));
                station.ObservationSystem =
                    ParseValueForObservationSystem(line.Substring(74, 1));
                station.OfficeType =
                    ParseValueForOfficeType(line.Substring(77, 1));

                if (int.TryParse(line.Substring(79, 1), out temp))
                {
                    station.Priority = temp;
                }

                station.Country = line.Substring(81, 2);
            }
            catch (Exception ex)
            {
                string message = string.Format("Parsing error on line: {0}", line);
                throw new ParserException(message, ex);
            }
            return station;
        }

        #endregion Data Parsing

        #region Text to Enumeration

        /// <summary>
        /// Converts a text value to METAR enumeration if possible
        /// </summary>
        /// <param name="value">String to convert to an enum value</param>
        /// <returns>Unknown if it can't convert the string to an enum value</returns>
        public Enumerations.METAR ParseValueForMETAR(string value)
        {
            Enumerations.METAR metar = Enumerations.METAR.Unknown;
            string cleanValue = value.Trim().ToUpper();
            if (string.IsNullOrEmpty(cleanValue))
            {
                metar = Enumerations.METAR.Empty;
            }
            else
            {
                if (!Enum.TryParse<Enumerations.METAR>(cleanValue, out metar))
                {
                    metar = Enumerations.METAR.Unknown;
                }
            }

            return metar;
        }

        /// <summary>
        /// Converts a text value to AviationFlag enumeration if possible
        /// </summary>
        /// <param name="value">String to convert to an enum value</param>
        /// <returns>Unknown if it can't convert the string to an enum value</returns>
        public Enumerations.AviationFlag ParseValueForAviationFlag(string value)
        {
            Enumerations.AviationFlag aviationFlag = Enumerations.AviationFlag.Unknown;
            string cleanValue = value.Trim().ToUpper();
            if (string.IsNullOrEmpty(cleanValue))
            {
                aviationFlag = Enumerations.AviationFlag.Empty;
            }
            else
            {
                if (!Enum.TryParse<Enumerations.AviationFlag>(cleanValue, out aviationFlag))
                {
                    aviationFlag = Enumerations.AviationFlag.Unknown;
                }
            }

            return aviationFlag;
        }

        /// <summary>
        /// Converts a text value to ObservationSystem enumeration if possible
        /// </summary>
        /// <param name="value">String to convert to an enum value</param>
        /// <returns>Unknown if it can't convert the string to an enum value</returns>
        public Enumerations.ObservationSystem ParseValueForObservationSystem(string value)
        {
            Enumerations.ObservationSystem obsSystem = Enumerations.ObservationSystem.Unknown;
            string cleanValue = value.Trim().ToUpper();
            if (string.IsNullOrEmpty(cleanValue))
            {
                obsSystem = Enumerations.ObservationSystem.Empty;
            }
            else
            {
                if (!Enum.TryParse<Enumerations.ObservationSystem>(cleanValue, out obsSystem))
                {
                    obsSystem = Enumerations.ObservationSystem.Unknown;
                }
            }

            return obsSystem;
        }

        /// <summary>
        /// Converts a text value to OfficeType enumeration if possible
        /// </summary>
        /// <param name="value">String to convert to an enum value</param>
        /// <returns>Unknown if it can't convert the string to an enum value</returns>
        public Enumerations.OfficeType ParseValueForOfficeType(string value)
        {
            Enumerations.OfficeType officeType = Enumerations.OfficeType.Unknown;
            string cleanValue = value.Trim().ToUpper();
            if (string.IsNullOrEmpty(cleanValue))
            {
                officeType = Enumerations.OfficeType.Empty;
            }
            else
            {
                if (!Enum.TryParse<Enumerations.OfficeType>(cleanValue, out officeType))
                {
                    officeType = Enumerations.OfficeType.Unknown;
                }
            }

            return officeType;
        }

        /// <summary>
        /// Converts a text value to RADAR enumeration if possible
        /// </summary>
        /// <param name="value">String to convert to an enum value</param>
        /// <returns>Unknown if it can't convert the string to an enum value</returns>
        public Enumerations.RADAR ParseValueForRADAR(string value)
        {
            Enumerations.RADAR radar = Enumerations.RADAR.Unknown;
            string cleanValue = value.Trim().ToUpper();
            if (string.IsNullOrEmpty(cleanValue))
            {
                radar = Enumerations.RADAR.Empty;
            }
            else
            {
                if (!Enum.TryParse<Enumerations.RADAR>(cleanValue, out radar))
                {
                    radar = Enumerations.RADAR.Unknown;
                }
            }

            return radar;
        }

        /// <summary>
        /// Converts a text value to UpperAir enumeration if possible
        /// </summary>
        /// <param name="value">String to convert to an enum value</param>
        /// <returns>Unknown if it can't convert the string to an enum value</returns>
        public Enumerations.UpperAir ParseValueForUpperAir(string value)
        {
            Enumerations.UpperAir upperAir = Enumerations.UpperAir.Unknown;
            string cleanValue = value.Trim().ToUpper();
            if (string.IsNullOrEmpty(cleanValue))
            {
                upperAir = Enumerations.UpperAir.Empty;
            }
            else
            {
                if (!Enum.TryParse<Enumerations.UpperAir>(cleanValue, out upperAir))
                {
                    upperAir = Enumerations.UpperAir.Unknown;
                }
            }

            return upperAir;
        }

        #endregion Text to Enumeration

        #region Private Methods

        /// <summary>
        /// Splits the raw text based on new line characters
        /// </summary>
        /// <param name="rawText">Text to split</param>
        /// <returns>An empty array if the text is empty or only new line characters</returns>
        private string[] SplitTextOnLines(string rawText)
        {
            return rawText.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        }

        #endregion Private Methods
    }
}