namespace StationInfo.Parser.Library
{
    /*
     !   CD = 2 letter state (province) abbreviation
     !   STATION = 16 character station long name
     !   ICAO = 4-character international id
     !   IATA = 3-character (FAA) id
     !   SYNOP = 5-digit international synoptic number
     !   LAT = Latitude (degrees minutes)
     !   LON = Longitude (degree minutes)
     !   ELEV = Station elevation (meters)
     !   M = METAR reporting station.   Also Z=obsolete? site
     !   N = NEXRAD (WSR-88D) Radar site
     !   V = Aviation-specific flag (V=AIRMET/SIGMET end point, A=ARTCC T=TAF U=T+V)
     !   U = Upper air (rawinsonde=X) or Wind Profiler (W) site
     !   A = Auto (A=ASOS, W=AWOS, M=Meso, H=Human, G=Augmented) (H/G not yet impl.)
     !   C = Office type F=WFO/R=RFC/C=NCEP Center
     !   Digit that follows is a priority for plotting (0=highest)
     !   Country code (2-char) is last column
    */

    public class Station
    {
        public string StationName { get; set; }

        public string StateCountryName { get; set; }

        public string StateProvinceAbbrev { get; set; }

        public string ICAO { get; set; }

        public string IATA { get; set; }

        public int SYNOP { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public int Elevation { get; set; }

        public Enumerations.METAR METAR { get; set; }

        public Enumerations.RADAR RADAR { get; set; }

        public Enumerations.AviationFlag AviationFlag { get; set; }

        public Enumerations.UpperAir UpperAir { get; set; }

        public Enumerations.ObservationSystem ObservationSystem { get; set; }

        public Enumerations.OfficeType OfficeType { get; set; }

        public string Country { get; set; }

        public int Priority { get; set; }

        public override bool Equals(object obj)
        {
            var station = obj as Station;
            if (obj == null)
            {
                return false;
            }

            return (StationName == null && station.StationName == null
                    || StationName != null && StationName.Equals(station.StationName))
                && (StateCountryName == null && station.StateCountryName == null
                    || StateCountryName != null && StateCountryName.Equals(station.StateCountryName))
                && (StateProvinceAbbrev == null && station.StateProvinceAbbrev == null
                    || StateProvinceAbbrev != null && StateProvinceAbbrev.Equals(station.StateProvinceAbbrev))
                && (ICAO == null && station.ICAO == null
                    || ICAO != null && ICAO.Equals(station.ICAO))
                && (IATA == null && station.IATA == null
                    || IATA != null && IATA.Equals(station.IATA))
                && SYNOP == station.SYNOP
                && (Latitude == null && station.Latitude == null
                    || Latitude != null && Latitude.Equals(station.Latitude))
                && (Longitude == null && station.Longitude == null
                    || Longitude != null && Longitude.Equals(station.Longitude))
                && Elevation == station.Elevation
                && METAR == station.METAR
                && RADAR == station.RADAR
                && AviationFlag == station.AviationFlag
                && UpperAir == station.UpperAir
                && ObservationSystem == station.ObservationSystem
                && OfficeType == station.OfficeType
                && (Country == null && station.Country == null
                    || Country != null && Country.Equals(station.Country))
                && Priority == Priority;
        }
    }
}