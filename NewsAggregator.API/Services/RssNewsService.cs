using System.ServiceModel.Syndication;
using System.Xml;
using NewsAggregator.API.Models;

namespace NewsAggregator.API.Services
{
    public class RssNewsService : INewsService
    {
        public string ServiceName => "RSS Aggregator";

        private readonly Dictionary<string, (string url, string category, string country, string sourceType)> _rssFeeds;
        private readonly ILogger<RssNewsService> _logger;

        public RssNewsService(ILogger<RssNewsService> logger)
        {
            _logger = logger;

            _rssFeeds = new Dictionary<string, (string, string, string, string)>
            {
                // ==================== ЭКОЛОГИЯ ====================
                {"UN Environment Programme", ("https://www.unep.org/rss.xml", "Ecology", "un", "Research")},
                {"World Resources Institute", ("https://www.wri.org/feed", "Ecology", "us", "Research")},
                {"Environmental Protection Agency", ("https://www.epa.gov/newsreleases/search/rss", "Ecology", "us", "Research")},
                {"Greenpeace International", ("https://www.greenpeace.org/international/rss/", "Ecology", "nl", "News")},
                {"The Ecologist", ("https://theecologist.org/rss.xml", "Ecology", "gb", "News")},
                {"EcoWatch", ("https://www.ecowatch.com/news/feed", "Ecology", "us", "News")},
                {"Mongabay Environmental News", ("https://feeds.feedburner.com/MongabayEnvironmentalNews", "Ecology", "us", "News")},
                {"Environmental Health News", ("https://www.ehn.org/rss.xml", "Ecology", "us", "Research")},
                {"Climate Home News", ("https://www.climatechangenews.com/feed/", "Ecology", "gb", "News")},
                {"Carbon Brief", ("https://www.carbonbrief.org/feed", "Ecology", "gb", "Research")},
                {"Inside Climate News", ("https://insideclimatenews.org/feed/", "Ecology", "us", "News")},
                {"Grist Environmental News", ("https://grist.org/feed/", "Ecology", "us", "News")},
                {"Treehugger", ("https://www.treehugger.com/rss.xml", "Ecology", "us", "News")},
                {"Conservation International", ("https://www.conservation.org/rss/news", "Ecology", "us", "Research")},
                {"WWF News", ("https://www.worldwildlife.org/rss/news", "Ecology", "us", "Research")},
                {"Nature Conservancy", ("https://www.nature.org/rss.xml", "Ecology", "us", "Research")},
                {"Sierra Club", ("https://www.sierraclub.org/rss.xml", "Ecology", "us", "News")},
                {"Environmental Defense Fund", ("https://www.edf.org/rss.xml", "Ecology", "us", "Research")},
                {"Union of Concerned Scientists", ("https://www.ucsusa.org/rss/news", "Ecology", "us", "Research")},
                {"Climate Central", ("https://www.climatecentral.org/rss/news", "Ecology", "us", "Research")},
                {"Yale Environment 360", ("https://e360.yale.edu/rss", "Ecology", "us", "Research")},

                // Международные экологические источники
                {"China Dialogue - Ecology", ("https://chinadialogue.net/en/feed/", "Ecology", "cn", "News")},
                {"Eco-Business Asia", ("https://www.eco-business.com/rss", "Ecology", "sg", "News")},
                {"Down To Earth India", ("https://www.downtoearth.org.in/rss", "Ecology", "in", "News")},
                {"The Third Pole Environment", ("https://www.thethirdpole.net/en/rss/", "Ecology", "gb", "News")},
                {"African Conservation", ("https://africanconservation.org/feed/", "Ecology", "za", "News")},

                // Китайские экологические источники (ИСПРАВЛЕННЫЕ)
                {"China Environment News", ("http://english.mee.gov.cn/Resources/rss/news.xml", "Ecology", "cn", "News")},
                {"China Green Development", ("https://www.chinadaily.com.cn/rss/environment_rss.xml", "Ecology", "cn", "Research")},
                {"Peking University Environmental Research", ("https://english.pku.edu.cn/news/rss", "Ecology", "cn", "University")},
                {"Tsinghua University Climate Research", ("https://www.tsinghua.edu.cn/en/news/rss", "Ecology", "cn", "University")},

                // ==================== КРЕАТИВНЫЕ ИНДУСТРИИ ====================
                {"Design Week", ("https://www.designweek.co.uk/feed/", "CreativeIndustries", "gb", "News")},
                {"It's Nice That Creative", ("https://www.itsnicethat.com/rss", "CreativeIndustries", "gb", "News")},
                {"Creative Boom", ("https://www.creativeboom.com/feed/", "CreativeIndustries", "gb", "News")},
                {"Engineering.com", ("https://www.engineering.com/feeds/rss.ashx", "CreativeIndustries", "us", "News")},
                {"Fast Company Design", ("https://www.fastcompany.com/design/rss", "CreativeIndustries", "us", "News")},
                {"Creative Review", ("https://www.creativereview.co.uk/feed/", "CreativeIndustries", "gb", "News")},
                {"The Dieline", ("https://thedieline.com/blog/rss.xml", "CreativeIndustries", "us", "News")},
                {"Packaging of the World", ("https://packagingoftheworld.com/feed", "CreativeIndustries", "us", "News")},
                {"Brand New", ("https://www.underconsideration.com/brandnew/feed.xml", "CreativeIndustries", "us", "News")},
                {"Communication Arts", ("https://www.commarts.com/rss", "CreativeIndustries", "us", "News")},
                {"Print Magazine", ("https://www.printmag.com/feed/", "CreativeIndustries", "us", "News")},
                {"How Design", ("https://www.howdesign.com/feed/", "CreativeIndustries", "us", "News")},
                {"Adobe Creative Cloud", ("https://blog.adobe.com/en/feeds/rss.xml", "CreativeIndustries", "us", "News")},
                {"Behance Blog", ("https://www.behance.net/blog/feed", "CreativeIndustries", "us", "News")},
                {"Dribbble Stories", ("https://dribbble.com/stories/feed", "CreativeIndustries", "us", "News")},
                {"AIGA Eye on Design", ("https://eyeondesign.aiga.org/feed/", "CreativeIndustries", "us", "News")},
                {"Smashing Magazine", ("https://www.smashingmagazine.com/feed/", "CreativeIndustries", "de", "News")},
                {"Creative Bloq", ("https://www.creativebloq.com/feeds/rss", "CreativeIndustries", "gb", "News")},

                // Международные креативные индустрии
                {"Design Milk", ("https://design-milk.com/feed/", "CreativeIndustries", "us", "News")},
                {"Core77 Design", ("https://www.core77.com/posts/rss", "CreativeIndustries", "us", "News")},
                {"Dezeen", ("https://www.dezeen.com/feed/", "CreativeIndustries", "gb", "News")},
                {"Wallpaper Design", ("https://www.wallpaper.com/feed", "CreativeIndustries", "gb", "News")},
                {"Design Taxi", ("https://designtaxi.com/news.xml", "CreativeIndustries", "sg", "News")},
                {"Cool Hunting", ("https://coolhunting.com/feed/", "CreativeIndustries", "us", "News")},

                // Китайские креативные индустрии (ИСПРАВЛЕННЫЕ)
                {"China Design Centre", ("https://www.chinadaily.com.cn/rss/culture_rss.xml", "CreativeIndustries", "cn", "News")},
                {"Design China", ("https://www.scmp.com/rss/91/feed", "CreativeIndustries", "cn", "News")},
                {"Peking University Arts & Design", ("https://english.pku.edu.cn/news/rss", "CreativeIndustries", "cn", "University")},
                {"Tsinghua University Academy of Arts", ("https://www.tsinghua.edu.cn/en/news/rss", "CreativeIndustries", "cn", "University")},
                {"China Creative Industries", ("https://www.chinadaily.com.cn/rss/business_rss.xml", "CreativeIndustries", "cn", "Research")},

                // ==================== АРХИТЕКТУРА И ГОРОДСКАЯ СРЕДА ====================
                {"ArchDaily", ("https://www.archdaily.com/rss", "UrbanDevelopment", "us", "News")},
                {"Dezeen Architecture", ("https://www.dezeen.com/architecture/feed/", "UrbanDevelopment", "gb", "News")},
                {"The Architect's Newspaper", ("https://archpaper.com/feed/", "UrbanDevelopment", "us", "News")},
                {"World Architecture", ("https://worldarchitecture.org/rss/rss.xml", "UrbanDevelopment", "us", "News")},
                {"Urban Land Institute", ("https://uli.org/feed/", "UrbanDevelopment", "us", "Research")},
                {"Architect Magazine", ("https://www.architectmagazine.com/rss", "UrbanDevelopment", "us", "News")},
                {"Architectural Digest", ("https://www.architecturaldigest.com/rss", "UrbanDevelopment", "us", "News")},
                {"Architectural Record", ("https://www.architecturalrecord.com/rss", "UrbanDevelopment", "us", "News")},
                {"The Architectural Review", ("https://www.architectural-review.com/rss", "UrbanDevelopment", "gb", "News")},
                {"Building Design", ("https://www.bdonline.co.uk/rss", "UrbanDevelopment", "gb", "News")},
                {"Architizer", ("https://architizer.com/blog/feed/", "UrbanDevelopment", "us", "News")},
                {"Landscape Architecture Magazine", ("https://landscapearchitecturemagazine.org/feed/", "UrbanDevelopment", "us", "News")},
                {"The Dirt (ASLA)", ("https://dirt.asla.org/feed/", "UrbanDevelopment", "us", "News")},
                {"CityLab", ("https://www.citylab.com/feed/", "UrbanDevelopment", "us", "News")},
                {"Next City", ("https://nextcity.org/rss", "UrbanDevelopment", "us", "News")},
                {"Planetizen", ("https://www.planetizen.com/rss.xml", "UrbanDevelopment", "us", "News")},

                // Международная архитектура
                {"World Landscape Architecture", ("https://worldlandscapearchitect.com/feed/", "UrbanDevelopment", "au", "News")},
                {"Architecture & Design Australia", ("https://www.architectureanddesign.com.au/rss", "UrbanDevelopment", "au", "News")},
                {"Indian Architect & Builder", ("https://www.iab.com.au/feed/", "UrbanDevelopment", "in", "News")},
                {"Canadian Architect", ("https://www.canadianarchitect.com/feed/", "UrbanDevelopment", "ca", "News")},
                {"Architecture NZ", ("https://architecturenow.co.nz/feed/", "UrbanDevelopment", "nz", "News")},

                // Китайская архитектура и урбанистика (ИСПРАВЛЕННЫЕ)
                {"China Architecture News", ("https://www.chinadaily.com.cn/rss/life_rss.xml", "UrbanDevelopment", "cn", "News")},
                {"Peking University Urban Planning", ("https://english.pku.edu.cn/news/rss", "UrbanDevelopment", "cn", "University")},
                {"Tongji University Architecture", ("https://en.tongji.edu.cn/Data/List/rss", "UrbanDevelopment", "cn", "University")},
                {"China Urban Development", ("https://www.chinadaily.com.cn/rss/china_rss.xml", "UrbanDevelopment", "cn", "Research")},
                {"Shanghai Urban Planning", ("https://www.shine.cn/feed/", "UrbanDevelopment", "cn", "Research")},

                // ==================== ОБРАЗОВАТЕЛЬНЫЕ ТЕХНОЛОГИИ ====================
                {"EdSurge", ("https://www.edsurge.com/feed", "EducationTech", "us", "News")},
                {"The Journal", ("https://thejournal.com/rss/", "EducationTech", "us", "News")},
                {"eSchool News", ("https://www.eschoolnews.com/feed/", "EducationTech", "us", "News")},
                {"Campus Technology", ("https://campustechnology.com/rss-feeds/all.aspx", "EducationTech", "us", "News")},
                {"EdTech Magazine", ("https://edtechmagazine.com/rss.xml", "EducationTech", "us", "News")},
                {"EdTech Review", ("https://www.edtechreview.in/rss", "EducationTech", "in", "News")},
                {"E-Learning Industry", ("https://elearningindustry.com/feed", "EducationTech", "us", "News")},
                {"TeachThought", ("https://www.teachthought.com/feed/", "EducationTech", "us", "News")},
                {"Edutopia", ("https://www.edutopia.org/rss", "EducationTech", "us", "Research")},
                {"Getting Smart", ("https://www.gettingsmart.com/feed/", "EducationTech", "us", "News")},
                {"Education Dive", ("https://www.educationdive.com/feeds/rss/", "EducationTech", "us", "News")},
                {"EdTech Digest", ("https://www.edtechdigest.com/feed/", "EducationTech", "us", "News")},
                {"Education Technology", ("https://educationtechnologyinsights.com/rss/feeds/ciorss.xml", "EducationTech", "us", "News")},
                {"eLearning Inside", ("https://news.elearninginside.com/feed/", "EducationTech", "us", "News")},
                {"ICT & Computing in Education", ("https://www.ictineducation.org/feed/", "EducationTech", "gb", "News")},
                {"EdTech Hub", ("https://edtechhub.org/feed/", "EducationTech", "gb", "Research")},

                // Международные EdTech источники
                {"EduTech Today", ("https://edutech.com.tr/feed/", "EducationTech", "tr", "News")},
                {"EdTech Middle East", ("https://edtechmena.com/feed/", "EducationTech", "ae", "News")},
                {"Asia EdTech", ("https://asiaedtech.org/feed/", "EducationTech", "sg", "News")},
                {"European EdTech", ("https://www.europeanedtech.org/feed/", "EducationTech", "eu", "News")},

                // Китайские образовательные технологии (ИСПРАВЛЕННЫЕ)
                {"China Education Technology", ("https://www.chinadaily.com.cn/rss/edu_rss.xml", "EducationTech", "cn", "News")},
                {"Peking University Education Innovation", ("https://english.pku.edu.cn/news/rss", "EducationTech", "cn", "University")},
                {"Tsinghua University Online Education", ("https://www.tsinghua.edu.cn/en/news/rss", "EducationTech", "cn", "University")},
                {"China E-Learning", ("https://www.chinadaily.com.cn/rss/tech_rss.xml", "EducationTech", "cn", "Research")},
                {"East China Normal University EdTech", ("https://english.ecnu.edu.cn/Data/List/rss", "EducationTech", "cn", "University")},

                // ==================== МЕДИЦИНСКИЕ ТЕХНОЛОГИИ ====================
                {"Medical News Today", ("https://www.medicalnewstoday.com/rss", "HealthcareTech", "us", "News")},
                {"Healthcare IT News", ("https://www.healthcareitnews.com/rss.xml", "HealthcareTech", "us", "News")},
                {"MobiHealthNews", ("https://www.mobihealthnews.com/rss.xml", "HealthcareTech", "us", "News")},
                {"HealthTech Magazine", ("https://healthtechmagazine.net/rss.xml", "HealthcareTech", "us", "News")},
                {"Digital Health", ("https://www.digitalhealth.net/feed/", "HealthcareTech", "gb", "News")},
                {"Medical Device Network", ("https://www.medicaldevice-network.com/rss/", "HealthcareTech", "gb", "News")},
                {"Healthcare Dive", ("https://www.healthcaredive.com/feeds/rss/", "HealthcareTech", "us", "News")},
                {"Fierce Healthcare", ("https://www.fiercehealthcare.com/rss/xml", "HealthcareTech", "us", "News")},
                {"Health IT Analytics", ("https://healthitanalytics.com/rss", "HealthcareTech", "us", "News")},
                {"Health IT Security", ("https://healthitsecurity.com/rss", "HealthcareTech", "us", "News")},
                {"Medical Xpress - Medical Tech", ("https://medicalxpress.com/rss-feed/technology/", "HealthcareTech", "us", "News")},
                {"The Medical Futurist", ("https://medicalfuturist.com/feed/", "HealthcareTech", "hu", "News")},
                {"Healthcare Technology", ("https://www.healthcaretechnology.org/feed", "HealthcareTech", "us", "News")},
                {"MedTech Dive", ("https://www.medtechdive.com/feeds/rss/", "HealthcareTech", "us", "News")},
                {"Health Management", ("https://healthmanagement.org/rss", "HealthcareTech", "cy", "News")},

                // Международные медицинские технологии
                {"MedTech Europe", ("https://www.medtecheurope.org/news/feed/", "HealthcareTech", "eu", "News")},
                {"Asia Medical Technology", ("https://www.medicaltecheurope.asia/feed/", "HealthcareTech", "sg", "News")},
                {"Medical Technology Middle East", ("https://medicaltechnology.me/feed/", "HealthcareTech", "ae", "News")},
                {"Africa Health Tech", ("https://africahealthtech.com/feed/", "HealthcareTech", "za", "News")},

                // Китайские медицинские технологии (ИСПРАВЛЕННЫЕ)
                {"China Medical Technology", ("https://www.chinadaily.com.cn/rss/health_rss.xml", "HealthcareTech", "cn", "News")},
                {"Peking University Health Science", ("https://english.pku.edu.cn/news/rss", "HealthcareTech", "cn", "University")},
                {"Peking Union Medical College", ("https://english.pumc.edu.cn/Data/List/rss", "HealthcareTech", "cn", "University")},
                {"China Healthcare Innovation", ("https://www.chinadaily.com.cn/rss/sci_rss.xml", "HealthcareTech", "cn", "Research")},
                {"Shanghai Medical Technology", ("https://www.shine.cn/feed/", "HealthcareTech", "cn", "University")},

                // ==================== ТУРИЗМ ====================
                {"Travel Daily News", ("https://www.traveldailynews.com/rss.xml", "Tourism", "gr", "News")},
                {"World Tourism Organization", ("https://www.unwto.org/rss.xml", "Tourism", "es", "Research")},
                {"Skift", ("https://skift.com/feed/", "Tourism", "us", "News")},
                {"Travel Pulse", ("https://www.travelpulse.com/RSS/News", "Tourism", "us", "News")},
                {"Tourism Review", ("https://www.tourism-review.com/rss.xml", "Tourism", "ch", "News")},
                {"Travel Weekly", ("https://www.travelweekly.com/RSS", "Tourism", "us", "News")},
                {"Travel Agent Central", ("https://www.travelagentcentral.com/rss.xml", "Tourism", "us", "News")},
                {"Hospitality Net", ("https://www.hospitalitynet.org/rss", "Tourism", "nl", "News")},
                {"TTG Media", ("https://www.ttgmedia.com/rss", "Tourism", "gb", "News")},
                {"Breaking Travel News", ("https://www.breakingtravelnews.com/feed/", "Tourism", "gb", "News")},
                {"World Travel & Tourism Council", ("https://wttc.org/rss", "Tourism", "gb", "Research")},

                // Международный туризм
                {"Travel Daily Asia", ("https://www.traveldailymedia.com/feed/", "Tourism", "sg", "News")},
                {"eTurboNews", ("https://www.eturbonews.com/feed/", "Tourism", "us", "News")},
                {"Pacific Asia Travel Association", ("https://www.pata.org/feed/", "Tourism", "th", "Research")},
                {"Tourism Australia News", ("https://www.tourism.australia.com/en/rss/news-and-media-releases.html", "Tourism", "au", "Research")},
                {"Visit Britain News", ("https://www.visitbritain.org/rss.xml", "Tourism", "gb", "Research")},
                {"Tourism New Zealand", ("https://www.tourismnewzealand.com/rss/news", "Tourism", "nz", "Research")},

                // Китайский туризм (ИСПРАВЛЕННЫЕ)
                {"China Tourism News", ("https://www.chinadaily.com.cn/rss/travel_rss.xml", "Tourism", "cn", "News")},
                {"Peking University Tourism Research", ("https://english.pku.edu.cn/news/rss", "Tourism", "cn", "University")},
                {"China Travel and Tourism", ("https://www.chinadaily.com.cn/rss/lifestyle_rss.xml", "Tourism", "cn", "News")},
                {"Shanghai Tourism", ("https://www.shine.cn/feed/", "Tourism", "cn", "Research")},

                // ==================== УМНЫЙ ГОРОД ====================
                {"Smart Cities World", ("https://www.smartcitiesworld.net/rss", "SmartCity", "gb", "News")},
                {"Smart Cities Dive", ("https://www.smartcitiesdive.com/feeds/rss/", "SmartCity", "us", "News")},
                {"IoT World Today", ("https://www.iotworldtoday.com/feed", "SmartCity", "us", "News")},
                {"The Urban Technologist", ("https://theurbantechnologist.com/feed/", "SmartCity", "gb", "News")},
                {"Cities Today", ("https://cities-today.com/feed/", "SmartCity", "gb", "News")},
                {"Smart Cities Council", ("https://smartcitiescouncil.com/rss.xml", "SmartCity", "us", "Research")},
                {"IoT For All", ("https://www.iotforall.com/feed", "SmartCity", "us", "News")},
                {"Smart Energy International", ("https://www.smart-energy.com/feed/", "SmartCity", "nl", "News")},
                {"Intelligent Transport", ("https://www.intelligenttransport.com/feed/", "SmartCity", "gb", "News")},
                {"Urban Innovation", ("https://urbaninnovation.org/feed/", "SmartCity", "us", "Research")},
                {"City Monitor", ("https://citymonitor.ai/rss", "SmartCity", "gb", "News")},
                {"The City Fix", ("https://thecityfix.com/feed/", "SmartCity", "us", "Research")},

                // Международные умные города
                {"Smart City Lab", ("https://www.smartcitylab.com/feed/", "SmartCity", "es", "News")},
                {"Urban Tech", ("https://urban-tech.news/feed/", "SmartCity", "de", "News")},
                {"IoT Business News", ("https://iotbusinessnews.com/feed/", "SmartCity", "us", "News")},
                {"Smart City Hub", ("https://www.smartcityhub.com/feed/", "SmartCity", "ch", "News")},
                {"Urban Mobility", ("https://www.urbanmobility.org/feed/", "SmartCity", "be", "Research")},
                {"Asian Smart Cities", ("https://www.smartcitiesworld.net/asia-pacific/rss", "SmartCity", "sg", "News")},

                // Китайские умные города (ИСПРАВЛЕННЫЕ)
                {"China Smart City", ("https://www.chinadaily.com.cn/rss/sci-tech_rss.xml", "SmartCity", "cn", "News")},
                {"Peking University Smart City Research", ("https://english.pku.edu.cn/news/rss", "SmartCity", "cn", "University")},
                {"Tsinghua University IoT Research", ("https://www.tsinghua.edu.cn/en/news/rss", "SmartCity", "cn", "University")},
                {"China Urban Technology", ("https://www.chinadaily.com.cn/rss/biz_rss.xml", "SmartCity", "cn", "Research")},

                // ==================== КУЛЬТУРА И ИСКУССТВО (ИСПРАВЛЕННЫЕ) ====================
                {"Artnet News", ("https://news.artnet.com/feed", "Culture", "us", "News")},
                {"Hyperallergic", ("https://hyperallergic.com/feed/", "Culture", "us", "News")},
                {"The Art Newspaper", ("https://www.theartnewspaper.com/rss", "Culture", "gb", "News")},
                {"Artsy", ("https://www.artsy.net/rss", "Culture", "us", "News")},
                {"Art News", ("https://www.artnews.com/feed/", "Culture", "us", "News")},
                {"Artforum News", ("https://www.artforum.com/feed", "Culture", "us", "News")},
                {"Contemporary Art Daily", ("https://www.contemporaryartdaily.com/feed/", "Culture", "us", "News")},
                {"Colossal", ("https://www.thisiscolossal.com/feed/", "Culture", "us", "News")},
                {"Design Milk Culture", ("https://design-milk.com/category/art/feed/", "Culture", "us", "News")},
                {"The Guardian Culture", ("https://www.theguardian.com/uk/culture/rss", "Culture", "gb", "News")},
                {"BBC Culture", ("https://feeds.bbci.co.uk/news/entertainment_and_arts/rss.xml", "Culture", "gb", "News")},
                {"The New York Times Arts", ("https://rss.nytimes.com/services/xml/rss/nyt/Arts.xml", "Culture", "us", "News")},
                {"Los Angeles Times Arts", ("https://www.latimes.com/entertainment-arts/rss", "Culture", "us", "News")},
                {"Frieze", ("https://www.frieze.com/rss", "Culture", "gb", "News")},
                {"Apollo Magazine", ("https://www.apollo-magazine.com/feed/", "Culture", "gb", "News")},

                // Международная культура (ИСПРАВЛЕННЫЕ)
                {"Art Asia Pacific", ("https://artasiapacific.com/feed", "Culture", "hk", "News")},
                {"The Culture Trip", ("https://theculturetrip.com/feed/", "Culture", "gb", "News")},
                {"Art Review", ("https://artreview.com/rss", "Culture", "gb", "News")},
                {"Korean Art News", ("https://www.koreaherald.com/rss.php", "Culture", "kr", "News")},
                {"Japan Arts News", ("https://www.japantimes.co.jp/feed/", "Culture", "jp", "News")},

                // Китайская культура и искусство (ИСПРАВЛЕННЫЕ)
                {"China Culture News", ("https://www.chinadaily.com.cn/rss/culture_rss.xml", "Culture", "cn", "News")},
                {"Peking University Culture Research", ("https://english.pku.edu.cn/news/rss", "Culture", "cn", "University")},
                {"Peking University Arts", ("https://english.pku.edu.cn/news/rss", "Culture", "cn", "University")},
                {"China Art Museum", ("https://www.namoc.org/en/News/rss", "Culture", "cn", "Research")},
                {"Shanghai Culture", ("https://www.shine.cn/feed/", "Culture", "cn", "News")},
                {"Beijing Cultural Heritage", ("https://www.chinadaily.com.cn/rss/culture_rss.xml", "Culture", "cn", "Research")},

                // ==================== УНИВЕРСИТЕТСКИЕ ИСТОЧНИКИ ====================
                {"MIT News", ("http://news.mit.edu/rss/topic/environment", "Ecology", "us", "University")},
                {"Stanford Energy", ("https://energy.stanford.edu/news/feed", "Ecology", "us", "University")},
                {"Harvard Graduate School of Design", ("https://www.gsd.harvard.edu/feed/", "UrbanDevelopment", "us", "University")},
                {"Cambridge Architecture", ("https://www.arct.cam.ac.uk/news/rss", "UrbanDevelopment", "gb", "University")},
                {"Berkeley College of Environmental Design", ("https://ced.berkeley.edu/feed/", "UrbanDevelopment", "us", "University")},
                {"MIT Media Lab", ("https://www.media.mit.edu/rss/rss.xml", "CreativeIndustries", "us", "University")},
                {"Stanford d.school", ("https://dschool.stanford.edu/feed/", "CreativeIndustries", "us", "University")},
                {"Harvard Medical School", ("https://hms.harvard.edu/rss/news", "HealthcareTech", "us", "University")},
                {"Johns Hopkins Medicine", ("https://www.hopkinsmedicine.org/news/feeds/news", "HealthcareTech", "us", "University")},
                {"MIT Open Learning", ("https://openlearning.mit.edu/news/feed", "EducationTech", "us", "University")},
                {"Stanford Graduate School of Education", ("https://ed.stanford.edu/news/feed", "EducationTech", "us", "University")},
                {"Oxford Internet Institute", ("https://www.oii.ox.ac.uk/news/feed/", "SmartCity", "gb", "University")},
                {"Carnegie Mellon Smart Cities", ("https://www.cmu.edu/news/feeds/smart-cities.xml", "SmartCity", "us", "University")},
                {"UCLA Arts", ("https://www.arts.ucla.edu/feed/", "Culture", "us", "University")},
                {"Yale School of Art", ("https://www.art.yale.edu/rss.xml", "Culture", "us", "University")},

                // Международные университеты
                {"University of Tokyo Engineering", ("https://www.t.u-tokyo.ac.jp/soee/en/feed/", "CreativeIndustries", "jp", "University")},
                {"ETH Zurich Architecture", ("https://arch.ethz.ch/en/news-events/feed.xml", "UrbanDevelopment", "ch", "University")},
                {"Delft University of Technology", ("https://www.tudelft.nl/en/feed/", "UrbanDevelopment", "nl", "University")},
                {"National University of Singapore Design", ("https://www.sde.nus.edu.sg/feed/", "CreativeIndustries", "sg", "University")},
                {"University of Cambridge Engineering", ("https://www.eng.cam.ac.uk/news/rss", "CreativeIndustries", "gb", "University")},
                {"Imperial College London Environment", ("https://www.imperial.ac.uk/news/feed/", "Ecology", "gb", "University")},
                {"University of Toronto Medicine", ("https://www.utoronto.ca/news/feed", "HealthcareTech", "ca", "University")},
                {"University of Melbourne Culture", ("https://about.unimelb.edu.au/newsroom/feed", "Culture", "au", "University")},
                {"University of British Columbia Sustainability", ("https://sustain.ubc.ca/news/feed", "Ecology", "ca", "University")},
                {"University of Sydney Architecture", ("https://www.sydney.edu.au/architecture/feed.xml", "UrbanDevelopment", "au", "University")},
                {"University of Hong Kong Urban Planning", ("https://www.arch.hku.hk/feed/", "UrbanDevelopment", "hk", "University")},
                {"Seoul National University Engineering", ("https://en.snu.ac.kr/feed", "CreativeIndustries", "kr", "University")},
                {"University of Copenhagen Sustainability", ("https://www.science.ku.dk/english/news/feed/", "Ecology", "dk", "University")},

                // Китайские университеты (ИСПРАВЛЕННЫЕ)
                {"Peking University General News", ("https://english.pku.edu.cn/news/rss", "General", "cn", "University")},
                {"Tsinghua University News", ("https://www.tsinghua.edu.cn/en/news/rss", "General", "cn", "University")},
                {"Fudan University Research", ("https://www.fudan.edu.cn/en/research/rss", "Research", "cn", "University")},
                {"Zhejiang University Innovation", ("https://www.zju.edu.cn/english/news/rss", "CreativeIndustries", "cn", "University")},
                {"Shanghai Jiao Tong University", ("https://en.sjtu.edu.cn/news/rss", "EducationTech", "cn", "University")}
            };
        }

        public async Task<List<Article>> GetNewsAsync(NewsRequest request)
        {
            var allArticles = new List<Article>();
            var tasks = new List<Task<List<Article>>>();

            _logger.LogInformation("Начинаем сбор новостей. Категория: {Category}", request.Category);

            // Ограничиваем количество одновременно обрабатываемых источников для производительности
            var feedsToProcess = _rssFeeds.Where(feed => ShouldProcessFeed(feed.Value, request)).Take(40).ToList();

            _logger.LogInformation("Обрабатывается {Count} источников из {Total}", feedsToProcess.Count, _rssFeeds.Count);

            foreach (var feed in feedsToProcess)
            {
                tasks.Add(ProcessFeedAsync(feed.Key, feed.Value, request));

                // Увеличиваем задержку для снижения нагрузки
                if (tasks.Count % 5 == 0)
                {
                    await Task.Delay(200);
                }
            }

            var results = await Task.WhenAll(tasks);
            foreach (var articles in results)
            {
                allArticles.AddRange(articles);
            }

            _logger.LogInformation("Собрано {Count} новостей", allArticles.Count);

            return allArticles.OrderByDescending(a => a.PublishedAt)
                             .Take(request.MaxResults)
                             .ToList();
        }

        private bool ShouldProcessFeed((string url, string category, string country, string sourceType) feed, NewsRequest request)
        {
            // Фильтрация по стране
            if (request.Countries.Count > 0 && !request.Countries.Contains(feed.country))
                return false;

            // Фильтрация по категории
            if (!string.IsNullOrEmpty(request.Category) &&
                !string.Equals(feed.category, request.Category, StringComparison.OrdinalIgnoreCase))
                return false;

            // Фильтрация по типу источника
            if (request.SourceTypes.Count > 0 && !request.SourceTypes.Contains(feed.sourceType))
                return false;

            return true;
        }

        private async Task<List<Article>> ProcessFeedAsync(string sourceName, (string url, string category, string country, string sourceType) feed, NewsRequest request)
        {
            var articles = new List<Article>();

            try
            {
                using var httpClient = new HttpClient();
                // Увеличиваем таймаут для медленных серверов
                httpClient.Timeout = TimeSpan.FromSeconds(25);
                httpClient.DefaultRequestHeaders.Add("User-Agent", "NewsAggregator/3.0 (+https://github.com)");

                var response = await httpClient.GetAsync(feed.url);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStreamAsync();

                    // Настройки XML Reader для обработки DTD
                    var settings = new XmlReaderSettings
                    {
                        DtdProcessing = DtdProcessing.Parse, // Разрешаем DTD
                        MaxCharactersFromEntities = 1024
                    };

                    using var reader = XmlReader.Create(content, settings);
                    var syndicationFeed = SyndicationFeed.Load(reader);

                    foreach (var item in syndicationFeed.Items.Take(8)) // Уменьшаем лимит для производительности
                    {
                        if (!MatchesQuery(item, request.Query)) continue;
                        if (!InDateRange(item.PublishDate, request.From, request.To)) continue;

                        var article = CreateArticleFromFeedItem(item, sourceName, feed);
                        articles.Add(article);
                    }

                    _logger.LogDebug("Обработан источник {Source}: {Count} статей", sourceName, articles.Count);
                }
                else
                {
                    _logger.LogWarning("Не удалось загрузить {Source}. Status: {Status}", sourceName, response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Ошибка при обработке {Source}", sourceName);
            }

            return articles;
        }

        private Article CreateArticleFromFeedItem(SyndicationItem item, string sourceName, (string url, string category, string country, string sourceType) feed)
        {
            var content = item.Summary?.Text ?? item.Content?.ToString() ?? "";

            // Очищаем HTML теги из контента и ограничиваем длину
            content = System.Text.RegularExpressions.Regex.Replace(content, "<.*?>", string.Empty);
            if (content.Length > 400)
            {
                content = content.Substring(0, 400) + "...";
            }

            return new Article
            {
                Title = item.Title?.Text ?? "Без заголовка",
                Description = item.Summary?.Text ?? "",
                Content = content,
                Url = item.Links.FirstOrDefault()?.Uri?.ToString() ?? "",
                PublishedAt = item.PublishDate.UtcDateTime,
                SourceName = sourceName,
                Country = feed.country,
                Category = feed.category,
                Language = "en",
                SourceType = feed.sourceType
            };
        }

        private bool MatchesQuery(SyndicationItem item, string query)
        {
            if (string.IsNullOrEmpty(query)) return true;

            var searchText = $"{item.Title?.Text} {item.Summary?.Text}".ToLower();
            return searchText.Contains(query.ToLower());
        }

        private bool InDateRange(DateTimeOffset publishDate, DateTime? from, DateTime? to)
        {
            var date = publishDate.UtcDateTime;
            if (from.HasValue && date < from.Value) return false;
            if (to.HasValue && date > to.Value.AddDays(1)) return false;
            return true;
        }
    }
}