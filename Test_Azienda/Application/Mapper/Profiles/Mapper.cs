using AutoMapper;
using Test_Azienda.Application.DTO;
using Test_Azienda.Domain.Table;

namespace Test_Azienda.Application.Mapper.Profiles
{
    // Classe per la gestione delle entità tra i DTO e il DB
    public class Mapper
    {
        private static MapperConfiguration? mapConfiguration;
        private static IMapper? mapper;


        // Imposta la configurazione del mapper

        private static void Initialize()
        {
            mapConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProfiloAzienda>();
                cfg.AddProfile<ProfiloDipendente>();
            });
        }

        // Restituisce la configurazione del Mapper

        public static MapperConfiguration Configuration
        {
            get
            {
                if (mapConfiguration == null)
                    Initialize();
                return mapConfiguration;
            }
        }

        /// <summary>
        /// Genera un singleton della configurazione del Mapper.
        /// </summary>
        /// <returns>
        /// restituisce un oggetto di mapping configurato
        /// </returns>
        public static IMapper GetMapper()
        {
            if (mapConfiguration == null)
                Initialize();
            if (mapper == null)
                mapper = mapConfiguration.CreateMapper();
            return mapper;
        }

        /// <summary>
        /// Crea un'istanza di un a classe dato un oggetto di un'altro tipo
        /// </summary>
        /// <remarks>I tipi delle classi deveono essere mappati nella configurazione</remarks>
        /// <typeparam name="T">Tipo della classe</typeparam>
        /// <param name="obj">oggetto da cui mappare i dati</param>
        /// <returns></returns>
        public static T Map<T>(object obj)
        {
            return GetMapper().Map<T>(obj);
        }

        public static void Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            GetMapper().Map(source, destination);
        }

        /// <summary>
        /// Copia i dati da una classe ad un altra
        /// </summary>
        /// <typeparam name="S">Tipo della classe sorgente</typeparam>
        /// <typeparam name="D">Tipo della classe destinazione</typeparam>
        /// <param name="source">oggetto con i dati da copiare</param>
        /// <param name="destination">oggetto di destinazione dei dati</param>
        /// <returns></returns>
        public static D Copy<S, D>(S source, D destination)
        {
            return GetMapper().Map(source, destination);
        }

        public class ProfiloAzienda : Profile
        {
            public ProfiloAzienda()
            {
                CreateMap<Azienda, AziendaDto>();
                CreateMap<AziendaDto, Azienda>();
                CreateMap<Ruolo, RuoloDto>();
            }
        }

        public class ProfiloDipendente : Profile
        {
            public ProfiloDipendente()
            {
                CreateMap<Dipendente, DipendenteDto>();
                CreateMap<DipendenteAnagrafica, DipendenteAnagraficaDto>();
                CreateMap<Utente, UtenteDto>();
                CreateMap<UtenteDto, Utente>();
            }
        }
    }
}