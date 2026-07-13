using Flowerfinder.Models;

namespace Flowerfinder.Data
{
    // Fills an empty database with a starter catalog of common flowers.
    // Runs once at startup (see Program.cs); does nothing if flowers already exist.
    public static class SeedData
    {
        // Photos live in wwwroot/images/flowers (sources: PHOTO_CREDITS.md in repo root)
        private static readonly Dictionary<string, string> Photos = new()
        {
            ["Rose"] = "/images/flowers/rose.jpg",
            ["Tulip"] = "/images/flowers/tulip.jpg",
            ["Sunflower"] = "/images/flowers/sunflower.jpg",
            ["Lavender"] = "/images/flowers/lavender.jpg",
            ["Peony"] = "/images/flowers/peony.jpg",
            ["Moth Orchid"] = "/images/flowers/moth-orchid.jpg",
            ["Common Daisy"] = "/images/flowers/common-daisy.jpg",
            ["Dahlia"] = "/images/flowers/dahlia.jpg",
            ["Bearded Iris"] = "/images/flowers/bearded-iris.jpg",
            ["Marigold"] = "/images/flowers/marigold.jpg",
            ["Daffodil"] = "/images/flowers/daffodil.jpg",
            ["Hyacinth"] = "/images/flowers/hyacinth.jpg",
            ["Chrysanthemum"] = "/images/flowers/chrysanthemum.jpg",
            ["Camellia"] = "/images/flowers/camellia.jpg",
            ["Magnolia"] = "/images/flowers/magnolia.jpg",
            ["Jasmine"] = "/images/flowers/jasmine.jpg",
            ["Cosmos"] = "/images/flowers/cosmos.jpg",
            ["Zinnia"] = "/images/flowers/zinnia.jpg",
            ["Madonna Lily"] = "/images/flowers/madonna-lily.jpg",
            ["Pansy"] = "/images/flowers/pansy.jpg"
        };

        public static void EnsureSeeded(AppDbContext db)
        {
            SeedFlowers(db);

            // Catalog expansion: insert any newer flowers not in the database yet
            var existingNames = db.Flowers.Select(f => f.CommonName).ToHashSet();
            var newcomers = MoreFlowers.All().Where(f => !existingNames.Contains(f.CommonName)).ToList();
            if (newcomers.Count > 0)
            {
                var now = DateTime.UtcNow;
                newcomers.ForEach(f => f.DateAdded = now);
                db.Flowers.AddRange(newcomers);
                db.SaveChanges();
            }

            // Backfill photos for known flowers that don't have one yet —
            // covers databases seeded before the photos existed.
            var missing = db.Flowers.Where(f => f.ImagePath == null).ToList();
            foreach (var f in missing)
            {
                if (Photos.TryGetValue(f.CommonName, out var path))
                    f.ImagePath = path;
            }
            if (missing.Count > 0) db.SaveChanges();

            // Backfill step-by-step growing plans for flowers that lack one
            var withoutPlan = db.Flowers.Where(f => f.GrowingPlan == null).ToList();
            var planned = 0;
            foreach (var f in withoutPlan)
            {
                if (GrowPlans.All.TryGetValue(f.CommonName, out var plan))
                {
                    f.GrowingPlan = plan.Trim();
                    planned++;
                }
            }
            if (planned > 0) db.SaveChanges();

            // Backfill in-depth guides for the flagship flowers
            var withoutGuide = db.Flowers.Where(f => f.GuideSections == null).ToList();
            var guided = 0;
            foreach (var f in withoutGuide)
            {
                if (DeepGuides.All.TryGetValue(f.CommonName, out var g))
                {
                    f.GuideSections = g.Sections.Trim();
                    f.CareCalendar = g.Calendar.Trim();
                    f.Problems = g.Problems.Trim();
                    f.Faqs = g.Faqs.Trim();
                    f.GearList = g.Gear.Trim();
                    guided++;
                }
            }
            if (guided > 0) db.SaveChanges();
        }

        private static void SeedFlowers(AppDbContext db)
        {
            if (db.Flowers.Any()) return;

            var now = DateTime.UtcNow;
            db.Flowers.AddRange(
                new Flower
                {
                    CommonName = "Rose", ScientificName = "Rosa gallica", Family = "Rosaceae",
                    Description = "The rose has stood for love for over two thousand years — Romans showered banquet guests with its petals, and a rose hung over a meeting table meant everything said stayed secret (\"sub rosa\"). More poems have been written about it than any other flower.",
                    CareGuide = "Plant in rich, well-drained soil with at least six hours of sun. Water deeply at the base rather than overhead to keep leaves dry, and prune in early spring to encourage strong new growth.",
                    Sunlight = Sunlight.FullSun, Watering = Watering.Moderate, BloomSeason = BloomSeason.Summer,
                    SoilType = "Rich, well-drained loam", Colors = "Red, Pink, White, Yellow", NativeRegion = "Asia and Europe",
                    IsPerennial = true, DateAdded = now
                },
                new Flower
                {
                    CommonName = "Tulip", ScientificName = "Tulipa gesneriana", Family = "Liliaceae",
                    Description = "In 1630s Holland a single tulip bulb could cost more than a canal house — history's first great market bubble, \"tulip mania.\" The flower actually hails from the mountains of Central Asia and was beloved by Ottoman sultans long before it reached Europe.",
                    CareGuide = "Plant bulbs in autumn, about three times as deep as the bulb is tall. They need cold winters to bloom well and prefer to stay fairly dry in summer.",
                    Sunlight = Sunlight.FullSun, Watering = Watering.Low, BloomSeason = BloomSeason.Spring,
                    SoilType = "Sandy, well-drained", Colors = "Red, Yellow, Pink, Purple, White", NativeRegion = "Central Asia",
                    IsPerennial = true, DateAdded = now
                },
                new Flower
                {
                    CommonName = "Sunflower", ScientificName = "Helianthus annuus", Family = "Asteraceae",
                    Description = "Young sunflowers physically turn to follow the sun across the sky each day, resetting east overnight — a motion called heliotropism. Native peoples of North America domesticated them over 4,000 years ago for food, dye and oil.",
                    CareGuide = "Sow seeds directly outside after the last frost in a sunny, sheltered spot. Tall varieties may need staking; water regularly while the flower head forms.",
                    Sunlight = Sunlight.FullSun, Watering = Watering.Moderate, BloomSeason = BloomSeason.Summer,
                    SoilType = "Any well-drained soil", Colors = "Yellow, Orange", NativeRegion = "North America",
                    IsPerennial = false, DateAdded = now
                },
                new Flower
                {
                    CommonName = "Lavender", ScientificName = "Lavandula angustifolia", Family = "Lamiaceae",
                    Description = "Romans scented their baths with it — the name comes from \"lavare,\" to wash. Fields of it paint Provence purple every July, and its oil has been used to calm nerves and perfume linens for centuries.",
                    CareGuide = "Loves poor, gravelly soil and hates wet feet — drainage is everything. Trim after flowering to keep plants compact, and avoid feeding; rich soil makes it floppy and less fragrant.",
                    Sunlight = Sunlight.FullSun, Watering = Watering.Low, BloomSeason = BloomSeason.Summer,
                    SoilType = "Poor, gravelly, alkaline", Colors = "Purple", NativeRegion = "Mediterranean",
                    IsPerennial = true, DateAdded = now
                },
                new Flower
                {
                    CommonName = "Peony", ScientificName = "Paeonia lactiflora", Family = "Paeoniaceae",
                    Description = "Named for Paeon, physician to the Greek gods, and treasured in China for over a thousand years as the \"king of flowers.\" Peony plants routinely outlive the gardeners who plant them — fifty years or more in one spot.",
                    CareGuide = "Plant with the buds no more than five centimetres below the surface — too deep and it won't flower. Be patient: it may sulk for a year or two after moving, then bloom reliably for decades.",
                    Sunlight = Sunlight.FullSun, Watering = Watering.Moderate, BloomSeason = BloomSeason.Spring,
                    SoilType = "Fertile, well-drained", Colors = "Pink, White, Red", NativeRegion = "East Asia",
                    IsPerennial = true, DateAdded = now
                },
                new Flower
                {
                    CommonName = "Moth Orchid", ScientificName = "Phalaenopsis amabilis", Family = "Orchidaceae",
                    Description = "Named for its resemblance to a moth in flight, this is the orchid most likely sitting on a windowsill near you. In the wild it grows not in soil but clinging to rainforest trees, drinking humidity from the air.",
                    CareGuide = "Pot in bark, never soil, and water by soaking then draining fully — roots that stay wet will rot. Bright light but no direct midday sun. When flowers drop, cut the stem above a node and it often reblooms.",
                    Sunlight = Sunlight.PartialShade, Watering = Watering.Moderate, BloomSeason = BloomSeason.YearRound,
                    SoilType = "Orchid bark mix (no soil)", Colors = "White, Pink, Purple", NativeRegion = "Southeast Asia",
                    IsPerennial = true, DateAdded = now
                },
                new Flower
                {
                    CommonName = "Common Daisy", ScientificName = "Bellis perennis", Family = "Asteraceae",
                    Description = "The name means \"day's eye\" — the flower opens at dawn and closes at dusk. \"He loves me, he loves me not\" has been played with its petals since medieval times, and lawns full of daisies were once a sign of a healthy meadow.",
                    CareGuide = "Practically grows itself in any moist lawn or border. Deadhead to extend flowering, or simply let it naturalise in grass.",
                    Sunlight = Sunlight.FullSun, Watering = Watering.Moderate, BloomSeason = BloomSeason.Spring,
                    SoilType = "Any moist soil", Colors = "White, Pink", NativeRegion = "Europe",
                    IsPerennial = true, DateAdded = now
                },
                new Flower
                {
                    CommonName = "Dahlia", ScientificName = "Dahlia pinnata", Family = "Asteraceae",
                    Description = "Mexico's national flower, grown by the Aztecs long before Europeans arrived — they used the hollow stems of tree dahlias as water pipes. Modern varieties range from pom-poms the size of a coin to \"dinner plate\" blooms wider than your hand.",
                    CareGuide = "Plant tubers after all frost danger has passed. Pinch out the growing tip when young for a bushier plant, and in cold climates lift and store tubers indoors over winter.",
                    Sunlight = Sunlight.FullSun, Watering = Watering.Moderate, BloomSeason = BloomSeason.Summer,
                    SoilType = "Rich, well-drained", Colors = "Red, Pink, Orange, Yellow, Purple, White", NativeRegion = "Mexico",
                    IsPerennial = true, DateAdded = now
                },
                new Flower
                {
                    CommonName = "Bearded Iris", ScientificName = "Iris germanica", Family = "Iridaceae",
                    Description = "Iris was the Greek goddess of the rainbow, and the flower delivers on the name — it blooms in nearly every colour. The French fleur-de-lis, symbol of kings, is a stylised iris.",
                    CareGuide = "Plant rhizomes shallowly with their tops showing at the surface — burying them causes rot. Divide clumps every three or four years after flowering to keep them vigorous.",
                    Sunlight = Sunlight.FullSun, Watering = Watering.Low, BloomSeason = BloomSeason.Spring,
                    SoilType = "Well-drained, neutral", Colors = "Purple, Blue, White, Yellow", NativeRegion = "Southern Europe",
                    IsPerennial = true, DateAdded = now
                },
                new Flower
                {
                    CommonName = "Marigold", ScientificName = "Tagetes erecta", Family = "Asteraceae",
                    Description = "The cempasúchil of Mexico's Día de los Muertos — its blazing orange petals are scattered in paths to guide spirits home. Gardeners plant it beside tomatoes because its roots repel soil pests.",
                    CareGuide = "One of the easiest annuals: sow after frost, give it sun, and deadhead spent blooms for flowers until autumn. Overwatering is the only common way to fail.",
                    Sunlight = Sunlight.FullSun, Watering = Watering.Low, BloomSeason = BloomSeason.Summer,
                    SoilType = "Any well-drained soil", Colors = "Orange, Yellow", NativeRegion = "Mexico",
                    IsPerennial = false, DateAdded = now
                },
                new Flower
                {
                    CommonName = "Daffodil", ScientificName = "Narcissus pseudonarcissus", Family = "Amaryllidaceae",
                    Description = "Named for Narcissus, the youth who fell in love with his own reflection — the flower nods forever toward the water. One of the first blooms of spring and the national flower of Wales, worn every St David's Day.",
                    CareGuide = "Plant bulbs in early autumn at twice their own depth. After flowering, let the leaves die back naturally for six weeks — they feed next year's bloom.",
                    Sunlight = Sunlight.FullSun, Watering = Watering.Moderate, BloomSeason = BloomSeason.Spring,
                    SoilType = "Moist but well-drained", Colors = "Yellow, White", NativeRegion = "Western Europe",
                    IsPerennial = true, DateAdded = now
                },
                new Flower
                {
                    CommonName = "Hyacinth", ScientificName = "Hyacinthus orientalis", Family = "Asparagaceae",
                    Description = "Greek myth says the flower sprang from the blood of Hyacinthus, a youth beloved by Apollo. Its perfume is so concentrated that a single stem can scent a whole room — Victorian homes forced bulbs indoors just for winter fragrance.",
                    CareGuide = "Plant bulbs in autumn about ten centimetres deep. For indoor blooms, chill bulbs in a dark cold place for ten weeks before bringing them into the light.",
                    Sunlight = Sunlight.FullSun, Watering = Watering.Moderate, BloomSeason = BloomSeason.Spring,
                    SoilType = "Well-drained, moderately fertile", Colors = "Purple, Pink, White, Blue", NativeRegion = "Eastern Mediterranean",
                    IsPerennial = true, DateAdded = now
                },
                new Flower
                {
                    CommonName = "Chrysanthemum", ScientificName = "Chrysanthemum morifolium", Family = "Asteraceae",
                    Description = "Cultivated in China for nearly 3,000 years and so revered in Japan that the emperor's throne is called the Chrysanthemum Throne. In much of Europe it is the flower of remembrance, laid on graves each November.",
                    CareGuide = "Pinch back stems until midsummer for compact, flower-covered plants. Give them sun, decent soil and good airflow to avoid mildew.",
                    Sunlight = Sunlight.FullSun, Watering = Watering.Moderate, BloomSeason = BloomSeason.Autumn,
                    SoilType = "Fertile, well-drained", Colors = "Yellow, Red, Pink, White, Orange, Purple", NativeRegion = "China",
                    IsPerennial = true, DateAdded = now
                },
                new Flower
                {
                    CommonName = "Camellia", ScientificName = "Camellia japonica", Family = "Theaceae",
                    Description = "The winter rose of Japan, blooming when almost nothing else dares. Coco Chanel made the white camellia her signature, pinning one to everything she wore. Its cousin, Camellia sinensis, gives the world every cup of tea.",
                    CareGuide = "Needs acidic soil and shelter from morning sun, which scorches frosted buds. Mulch generously and never let the roots dry out in summer — that's when next winter's buds form.",
                    Sunlight = Sunlight.PartialShade, Watering = Watering.Moderate, BloomSeason = BloomSeason.Winter,
                    SoilType = "Acidic, humus-rich", Colors = "Pink, Red, White", NativeRegion = "East Asia",
                    IsPerennial = true, DateAdded = now
                },
                new Flower
                {
                    CommonName = "Magnolia", ScientificName = "Magnolia grandiflora", Family = "Magnoliaceae",
                    Description = "Magnolias are living fossils — they evolved before bees existed, so their tough flowers were built to be pollinated by beetles. Fossilised magnolias over 20 million years old look almost identical to the ones blooming today.",
                    CareGuide = "Choose the planting spot carefully — magnolias hate being moved. Give them deep, slightly acidic soil, shelter from harsh wind, and room to grow for the next fifty years.",
                    Sunlight = Sunlight.FullSun, Watering = Watering.Moderate, BloomSeason = BloomSeason.Spring,
                    SoilType = "Deep, slightly acidic", Colors = "White, Pink", NativeRegion = "Southeastern United States",
                    IsPerennial = true, DateAdded = now
                },
                new Flower
                {
                    CommonName = "Jasmine", ScientificName = "Jasminum officinale", Family = "Oleaceae",
                    Description = "Jasmine saves its perfume for the night, releasing its strongest scent after dusk to summon moths. It takes around 8,000 hand-picked blossoms to make a single gram of jasmine absolute for perfume.",
                    CareGuide = "A vigorous climber that needs a trellis or wires and a warm, sheltered wall. Prune right after flowering; it blooms on the previous year's growth.",
                    Sunlight = Sunlight.FullSun, Watering = Watering.Moderate, BloomSeason = BloomSeason.Summer,
                    SoilType = "Fertile, well-drained", Colors = "White", NativeRegion = "Himalayas and South Asia",
                    IsPerennial = true, DateAdded = now
                },
                new Flower
                {
                    CommonName = "Cosmos", ScientificName = "Cosmos bipinnatus", Family = "Asteraceae",
                    Description = "Spanish priests in Mexico named it \"cosmos\" — Greek for order and harmony — for its perfectly even petals. Few flowers give more for less: one packet of seed fills a border with airy colour until the first frost.",
                    CareGuide = "Sow directly where it is to flower — it resents transplanting and poor soil actually makes it bloom harder. Deadhead often and it simply will not stop.",
                    Sunlight = Sunlight.FullSun, Watering = Watering.Low, BloomSeason = BloomSeason.Summer,
                    SoilType = "Poor to average, well-drained", Colors = "Pink, White, Magenta", NativeRegion = "Mexico",
                    IsPerennial = false, DateAdded = now
                },
                new Flower
                {
                    CommonName = "Zinnia", ScientificName = "Zinnia elegans", Family = "Asteraceae",
                    Description = "In January 2016, a zinnia aboard the International Space Station became the first flower ever grown and bloomed in space. On Earth it's a butterfly magnet that thrives on summer heat.",
                    CareGuide = "Sow after frost in full sun. Water at the base — wet leaves invite mildew — and cut flowers freely; every cut stem prompts two more.",
                    Sunlight = Sunlight.FullSun, Watering = Watering.Low, BloomSeason = BloomSeason.Summer,
                    SoilType = "Average, well-drained", Colors = "Red, Pink, Orange, Yellow, Purple, White", NativeRegion = "Mexico",
                    IsPerennial = false, DateAdded = now
                },
                new Flower
                {
                    CommonName = "Madonna Lily", ScientificName = "Lilium candidum", Family = "Liliaceae",
                    Description = "Cultivated for at least 3,000 years — it appears in Minoan frescoes on Crete — and long a symbol of purity in Renaissance paintings. Its scent deepens in the evening, carrying across a whole garden.",
                    CareGuide = "Unusually for a lily, plant this one shallowly, barely below the surface, in late summer. It makes a rosette of winter leaves, then sends up flower spikes in early summer.",
                    Sunlight = Sunlight.FullSun, Watering = Watering.Moderate, BloomSeason = BloomSeason.Summer,
                    SoilType = "Well-drained, slightly alkaline", Colors = "White", NativeRegion = "Balkans and Middle East",
                    IsPerennial = true, DateAdded = now
                },
                new Flower
                {
                    CommonName = "Pansy", ScientificName = "Viola × wittrockiana", Family = "Violaceae",
                    Description = "The name comes from the French \"pensée\" — a thought — because the flower's face seems lost in one. Shakespeare's Ophelia handed them out \"for thoughts,\" and their whiskered faces have charmed cottage gardens ever since.",
                    CareGuide = "A cool-season flower: plant in autumn or early spring, and deadhead constantly to keep blooms coming. Summer heat ends the show — replace with something heat-proof and replant in autumn.",
                    Sunlight = Sunlight.PartialShade, Watering = Watering.Moderate, BloomSeason = BloomSeason.Spring,
                    SoilType = "Moist, fertile", Colors = "Purple, Yellow, White, Blue", NativeRegion = "Europe (garden hybrid)",
                    IsPerennial = false, DateAdded = now
                }
            );
            db.SaveChanges();
        }
    }
}
