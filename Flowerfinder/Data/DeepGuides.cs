using Flowerfinder.Models;

namespace Flowerfinder.Data
{
    // In-depth growing guides for the flagship flowers, keyed by CommonName.
    // Backfilled by SeedData for flowers whose GuideSections is still null.
    //
    // Formats (see Flower.cs):
    //   Sections  — "## Title" starts a block; "- " bullets; "TIP:" / "WARN:" callouts
    //   Calendar  — 12 lines "Mon|action"           Problems — "Name|Symptom|Fix"
    //   Faqs      — "Question?|Answer"              Gear     — "Item|Why"
    //
    // Advice is standard, well-established horticulture (RHS-style, northern
    // hemisphere timing).
    public static class DeepGuides
    {
        public record Guide(string Sections, string Calendar, string Problems, string Faqs, string Gear);

        public static readonly Dictionary<string, Guide> All = new()
        {
            ["Rose"] = new(
                Sections: @"
## Soil & site
Roses are hungry, deep-rooted shrubs that repay good preparation for decades. Give them at least six hours of direct sun and a spot with moving air — still, crowded corners invite black spot and mildew.
- Dig in a bucket of well-rotted manure or compost per plant before planting
- Aim for rich, moisture-retentive but free-draining soil; roses sulk in pure sand and drown in waterlogged clay
- Avoid planting where an old rose grew (replant disease stunts newcomers) or swap out the soil generously
TIP: Morning sun matters most — it dries dew off the leaves early, which is your cheapest fungicide.

## Planting
Bare-root roses (November–March) establish best; potted roses can go in any time the ground isn't frozen or baked.
- Soak bare roots in a bucket of water for an hour before planting
- Dig a hole wide enough to spread the roots and deep enough that the graft union (the knuckle) sits at or just below soil level
- Firm the soil gently, water thoroughly, then mulch 5 cm deep, keeping mulch off the stems
WARN: Never let fresh manure touch the roots directly — it burns them. Mix it into the base of the hole and cover with plain soil first.

## Water & feeding
A deep soak once or twice a week beats a daily sprinkle — shallow watering trains roots to the surface where they scorch.
- Water at the base, not over the leaves, to keep fungal disease down
- Feed with a rose fertiliser in spring as growth starts, and again after the first flush of flowers
- Stop feeding by late summer so new growth can harden before frost

## Pruning & deadheading
Prune in late winter, just as buds swell. Cut about a third of the height away, always to an outward-facing bud, and remove the three Ds — dead, damaged, diseased — plus anything crossing.
- Deadhead spent blooms through summer to keep repeat-flowering varieties producing
- Cut flowers for the vase in the morning, above a five-leaflet leaf
TIP: A clean, sharp cut heals fast. Wipe secateurs with disinfectant between plants to avoid spreading disease.

## Winter care
Roses are tough, but a little tidying prevents spring trouble.
- Rake up and bin fallen rose leaves (never compost them) — black spot spores overwinter in leaf litter
- Shorten very tall stems by a third in autumn to stop wind-rock loosening the roots
- Mound compost over the graft union in cold regions",
                Calendar: @"Jan|Plant bare-root roses in mild spells
Feb|Finish winter pruning as buds swell
Mar|Feed, mulch, and clear last season's leaf litter
Apr|Watch new shoots for early aphids
May|First flush begins — deep-soak weekly in dry spells
Jun|Deadhead constantly; feed after the first flush
Jul|Water deeply in heat; check for black spot
Aug|Keep deadheading; stop feeding late in the month
Sep|Enjoy the second flush; let a few hips form
Oct|Rake and bin fallen leaves
Nov|Shorten tall stems against wind-rock; plant bare-root
Dec|Order next year's roses; prune only in frost-free spells",
                Problems: @"Black spot|Dark blotches on leaves that then yellow and drop|Bin affected leaves, improve airflow, water at the base only; spray with a rose fungicide from spring in bad years
Aphids|Sticky, curled shoot tips crowded with tiny green insects|Squash by hand or blast off with water; ladybirds arrive within weeks — avoid broad sprays that kill them too
Powdery mildew|White dusty coating on young leaves and buds|Water more consistently at the roots, prune for airflow, and remove the worst shoots
No flowers|Lush leafy growth but nothing blooms|Too much nitrogen or too much shade — switch to rose feed (higher potash) and check the site gets 6+ hours of sun",
                Faqs: @"How long do roses live?|Decades. A well-sited, well-fed rose commonly outlives the person who planted it — 30 years is unremarkable.
Can I grow a rose in a pot?|Yes — choose a patio or shrub rose, give it a 40 cm+ pot with loam-based compost, water often and feed fortnightly in summer.
Why are the ants on my rose?|Ants farm the aphids for honeydew. Deal with the aphids and the ants leave on their own.
Should I be scared of pruning?|No. Roses are famously hard to kill by pruning — a rough cut in the right season beats no cut at all.",
                Gear: @"Bypass secateurs|One clean cut heals; crushing cuts invite disease
Thorn-proof gloves|Gauntlet-style saves your forearms during pruning
Rose fertiliser|Higher potash than lawn feed — flowers, not just leaves
Mulch (compost or bark)|Locks in moisture and smothers black-spot spores
Watering can with a long spout|Water the base, not the leaves"),

            ["Tulip"] = new(
                Sections: @"
## Soil & site
Tulips come from sun-baked Central Asian hillsides — the closer you get to that, the longer they persist. Full sun and sharp drainage are non-negotiable; wet summer soil is why most tulips vanish after a year.
- Light, free-draining soil; work coarse grit into heavy clay
- A sheltered spot keeps tall varieties from snapping in spring gusts

## Planting
Plant bulbs in late autumn — November is ideal in most regions. The cold trick: late planting sidesteps tulip fire, the disease that thrives in warm, wet soil.
- Plant 15–20 cm deep (three times the bulb's height), pointed end up
- Space 8–10 cm apart; in pots you can pack them almost shoulder to shoulder
- Water once after planting, then leave them alone
TIP: Deep planting is the secret to tulips returning — shallow bulbs split into many tiny, non-flowering offsets.
WARN: Tulip bulbs are toxic to dogs and cats if eaten — store them out of reach.

## After the bloom
What you do after flowering decides whether the bulb blooms next year.
- Snap off the seed head once petals drop, but leave every leaf
- Let foliage die back naturally for six weeks — it's recharging the bulb
- Hold off watering once leaves yellow; the bulb wants a dry summer bake
- Fussy hybrids (parrots, doubles) are best treated as annuals; Darwin hybrids and species tulips reliably return

## Cutting for the vase
Tulips are the only common cut flower that keeps growing in water — up to 5 cm — which is why they droop so theatrically.
- Cut when the bud shows colour but hasn't opened
- Re-cut stems and use cold water; they last longer cool
- Wrap droopy stems tightly in newspaper in water for two hours to straighten them",
                Calendar: @"Jan|Pots of forced tulips can come indoors as shoots show
Feb|First species tulips emerge — do nothing, just watch
Mar|Early varieties bloom; slug-patrol emerging shoots
Apr|Peak tulip season — deadhead as petals fall
May|Late varieties finish; leave all foliage standing
Jun|Foliage yellows — stop watering, let bulbs bake
Jul|Lift and store fussy hybrids somewhere dry if you keep them
Aug|Order next spring's bulbs while the good ones are in stock
Sep|Prepare beds; add grit to heavy soil
Oct|Plant other spring bulbs — but make tulips wait
Nov|Tulip planting month: 15–20 cm deep, pointed end up
Dec|Finish planting in frost-free spells; mulch pots against hard freezes",
                Problems: @"Tulip fire|Twisted, scorched-looking shoots and spotted leaves|Dig up and bin affected bulbs; don't replant tulips in that spot for three years; plant late in cold soil
Squirrels dig up bulbs|Freshly planted bulbs excavated overnight|Lay chicken wire flat over the bed until shoots emerge, then remove; plant deep
Blind bulbs|Healthy leaves but no flower|Bulbs planted too shallow or exhausted — replant new bulbs deeper, feed after flowering next time
Bulb rot|Bulbs mushy in the ground, shoots absent|Drainage is the cause; add grit or switch to pots with free-draining compost",
                Faqs: @"Do tulips come back every year?|Species tulips and Darwin hybrids do if planted deep in free-draining soil with a dry summer. Fancy doubles and parrots fade — treat those as annuals.
Can I leave bulbs in pots?|One season of pot life is reliable; after that, plant them out into the garden in autumn and start the pot fresh.
Why did my tulips get shorter each year?|The bulb is splitting into offsets — a sign of shallow planting or summer moisture. Start again, deeper and drier.
When is it too late to plant?|As long as the ground isn't frozen solid, even a January planting usually flowers — just a little later.",
                Gear: @"Bulb planter or trowel with depth marks|15–20 cm is deeper than most people guess
Coarse horticultural grit|Turns heavy clay into tulip-friendly drainage
Chicken wire|The only squirrel deterrent that consistently works
Fresh bulbs each autumn|For show-stopping displays, new bulbs are the guarantee"),

            ["Sunflower"] = new(
                Sections: @"
## Soil & site
Sunflowers are uncomplicated: the sunniest spot you own, decent soil, and shelter from wind if you're chasing height. Their roots go surprisingly deep, so they tolerate dry spells better than they look like they should.
- Six-plus hours of direct sun; the tallest varieties want eight
- Dig compost into poor soil — giants are heavy feeders
- Plant along a fence or wall if you can; ready-made wind protection and something to tie to

## Sowing
Sow directly where they'll grow, after the last frost — sunflowers hate root disturbance, and direct-sown plants overtake transplants within weeks.
- Push seeds 2–3 cm deep, 30–45 cm apart (giants need 60 cm)
- Water well once, then keep just moist until seedlings show in 7–14 days
- Sow a few every fortnight until midsummer for months of continuous blooms
WARN: Slugs and snails consider sunflower seedlings a delicacy — protect until plants are 15 cm tall. A cut-down plastic bottle over each seedling works.

## Growing on
From knee-high onward, sunflowers mostly need water and something to lean on.
- Water deeply twice a week in dry weather — more when buds form
- Stake anything over 1.5 m: a sturdy cane and soft ties, added before it's needed
- Don't over-fertilise with nitrogen; you'll get a leafy giant with a small flower
TIP: The classic 'following the sun' movement is only in young plants. Mature heads permanently face east — plant where you'll see their faces in the morning.

## Seeds & the after-show
A single big head can carry a thousand seeds, and the goldfinches know it.
- For garden birds: leave heads standing and let them feed through autumn
- For roasting: cut heads when the back turns yellow-brown, hang somewhere airy for two weeks, then rub the seeds out
- The hollow stems make superb overwintering homes for beneficial insects — cut to 30 cm and leave standing rather than pulling",
                Calendar: @"Jan|Browse seed catalogues — giants, branching, or pollen-free for the vase
Feb|Nothing to do yet; sunflowers wait for warmth
Mar|Prepare the bed; dig in compost
Apr|Sow indoors in pots only if your springs are very short
May|Direct-sow after the last frost — the main event
Jun|Second sowing for succession; protect seedlings from slugs
Jul|Stake the climbers; deep water in dry spells
Aug|Peak bloom — enjoy; keep watering while heads develop
Sep|Late sowings flower; first heads ripen seed
Oct|Leave seed heads for the goldfinches
Nov|Cut stems to 30 cm for overwintering insects
Dec|Save the best head's seeds somewhere cool and dry",
                Problems: @"Vanishing seedlings|Sowed thickly, nothing but stumps remain|Slugs. Re-sow with bottle cloches over each station and patrol at dusk for the first three weeks
Drooping head and leaves|Whole plant wilts on hot afternoons|Usually thirst — a sunflower in bud drinks heavily. Soak deeply; it should recover overnight
Birds eat the seeds too soon|Head stripped before harvest|Tie a paper bag or fine mesh over ripening heads (plastic sweats and rots them)
Snapped stem|Tall plant broken at mid-height after wind|Stake earlier next time; a snapped-but-attached stem can sometimes be splinted with a cane and tape",
                Faqs: @"How tall will they really get?|Variety is everything: 'Russian Giant' can pass 3 m, dwarf types stay at 40 cm. The packet doesn't lie — pick accordingly.
Can I grow them in pots?|Dwarf varieties, happily — a 30 cm pot per plant. Giants in pots stay disappointingly small.
Why did my sunflower grow one flower and stop?|Classic giants are single-headed by nature. For armfuls of blooms, grow branching varieties.
Are they really easy enough for kids?|The easiest big result in gardening — seeds germinate in a week and growth is fast enough to measure daily.",
                Gear: @"Bamboo canes 2 m+ and soft ties|Anything over 1.5 m will need them
Bottle cloches|Slug-proof seedlings for free
Compost|Giants are hungry; poor soil means short plants
Paper bags or mesh|Save the seed harvest from the birds"),

            ["Lavender"] = new(
                Sections: @"
## Soil & site
Lavender is a Mediterranean shrub pretending to be a border flower. Full sun, lean soil, and sharp drainage keep it compact, fragrant, and alive; kindness — rich soil, extra water — kills it.
- Full sun, the hotter and brighter the better
- Free-draining, even poor, slightly alkaline soil; add plenty of grit to clay
- Raised beds, slopes and pot rims are lavender heaven
WARN: The number one lavender killer is wet feet in winter. If your soil holds water, grow it in a pot or a raised bed — not the ground.

## Planting
Plant in spring, once soil warms — spring planting gives a full season of root growth before the first wet winter.
- Space plants 30–40 cm apart (90 cm for hedging rows is too far; you want them to knit)
- Set the plant slightly proud of the soil so water sheds away from the crown
- Water weekly through the first summer only; after that it fends for itself

## Water & feeding
Once established, effectively none of either.
- Established plants need watering only in extreme drought
- No fertiliser: feeding produces floppy, scentless growth
- In pots: water when the compost is fully dry, and use terracotta if you can — it breathes
TIP: Grey, aromatic foliage is the drought-tolerance badge. If lavender looks lush and green, it's living too soft a life.

## Pruning
The non-negotiable. Unpruned lavender turns woody, splits open, and dies young; pruned lavender lives 15 years.
- Prune every year in late summer, right after flowers fade
- Cut back all the flower stems plus 2–3 cm of leafy growth beneath
- Shape into a tidy mound — think green hedgehog
WARN: Never cut into bare, leafless wood — old wood almost never re-shoots. If a plant is mostly bare stem, replace it rather than rescue it.

## Harvest & uses
Cut for drying when the bottom third of each flower spike has opened — that's peak oil.
- Cut in mid-morning after dew dries, bunch loosely, hang upside-down somewhere dark and airy for two weeks
- Rub dried flowers off the stems into jars for sachets, sleep pillows and baking",
                Calendar: @"Jan|Do nothing — especially don't water
Feb|Check pots aren't sitting in saucers of rain
Mar|Tidy any winter-scorched tips as growth resumes
Apr|Plant new lavender; take spring cuttings
May|First flower spikes rise on early varieties
Jun|Bloom begins in earnest — the bees arrive
Jul|Peak bloom; harvest for drying mid-month
Aug|Prune hard as flowers fade — the year's key job
Sep|Take semi-ripe cuttings as insurance
Oct|Clear fallen leaves off plants (trapped damp rots foliage)
Nov|Move pots against a sheltered wall
Dec|Admire the silver mound; plan next year's hedge",
                Problems: @"Sudden collapse|Whole plant grey and dead, often after a wet winter|Root rot from poor drainage — there's no cure. Replant elsewhere with grit, or use a pot
Woody and split open|Bare centre, sprawling leggy arms|Years of missed pruning. It won't re-shoot from old wood — take cuttings and start a fresh plant
Few flowers, floppy growth|Lush leaves, sparse scentless spikes|Too rich a soil, too much shade or fertiliser. Move it somewhere leaner and sunnier
Yellowing leaves|Foliage yellows in summer|Almost always overwatering, occasionally waterlogged soil — let it dry out completely",
                Faqs: @"Which lavender should I buy?|English lavender (angustifolia) is hardiest and best for cold or wet regions; French lavender (stoechas) blooms longer but dies in hard frosts.
How long does a plant live?|Pruned annually: 10–15 good years. Never pruned: woody and finished in 4–5.
Can I grow lavender from cuttings?|Very easily — 8 cm semi-ripe cuttings in gritty compost in late summer root within a month. One plant becomes a hedge in two years.
Is lavender safe for pets?|The plant itself is mildly toxic if eaten in quantity, but grazing incidents are rare — the taste puts animals off.",
                Gear: @"Horticultural grit|Drainage is life insurance for lavender
Hand shears|Fast, even mound-shaping at pruning time
Terracotta pots|Breathes and dries out — exactly what roots want
Twine and a dark airy shed|All drying requires"),

            ["Peony"] = new(
                Sections: @"
## Soil & site
A peony is a 50-year commitment that asks for one weekend of effort. Choose the spot as if you'll never move it — because you shouldn't.
- Full sun (six hours minimum) for the most flowers; light afternoon shade is fine in hot regions
- Rich, deep, well-drained soil; dig in compost generously before planting
- Keep clear of tree roots and thirsty hedges — peonies hate competition

## Planting — the depth rule
More peonies fail from deep planting than everything else combined. The 'eyes' — the red growth buds on the crown — must sit no more than 5 cm below the surface.
- Plant bare-root crowns in autumn (September–October), eyes up, 2–5 cm deep
- Space 90 cm apart; they get big
- Water in well, mulch lightly, and never bury the crown under mulch
WARN: Plant the eyes deeper than 5 cm and you'll get gorgeous healthy foliage and zero flowers — for years. If that's your peony, lift and replant it shallower in autumn.

## Water, feeding & support
Established peonies are drought-tolerant, low-maintenance perennials.
- Water deeply in dry spells during spring bud-up; ease off after flowering
- One handful of balanced fertiliser or a compost mulch each spring is plenty
- The huge double blooms need hoops: set peony rings over the clump in early spring, before stems reach 20 cm
TIP: The ants all over your buds are harvesting nectar and harming nothing. They're a peony tradition — let them be.

## After the bloom & winter
The foliage is next year's flower factory — protect it all summer.
- Deadhead spent blooms down to a strong leaf, but never cut healthy foliage in summer
- In autumn, once frost blackens the leaves, cut all stems to the ground and bin (don't compost) the foliage — this breaks the botrytis cycle
- No winter protection needed: peonies *require* winter cold to set flower buds

## Cutting for the vase
Cut at 'marshmallow stage' — buds coloured and soft like a marshmallow when squeezed.
- Morning cutting, stems into water immediately
- Marshmallow-stage buds stored dry in the fridge keep for weeks and open on demand",
                Calendar: @"Jan|Nothing to do — the cold is doing the work
Feb|Red shoots may nose through late in the month
Mar|Set support rings over emerging clumps; light feed
Apr|Buds swell; water if spring turns dry
May|The glorious two weeks — peak bloom
Jun|Deadhead; leave every healthy leaf
Jul|Water in drought; otherwise ignore
Aug|Foliage quietly feeds next year's buds
Sep|Planting and dividing month for bare roots
Oct|Finish planting; last chance to move a peony
Nov|After frost blackens foliage, cut to ground and bin it
Dec|Order bare roots for next autumn from specialists",
                Problems: @"No flowers, healthy leaves|Years of foliage, never a bud|Planted too deep — lift in autumn and replant with eyes 2–5 cm below the surface. Also check it's not in deep shade
Botrytis blight|Buds turn brown and mushy; stems blacken at the base|Cut out affected parts immediately, clear all autumn foliage religiously, improve airflow
Flopping blooms|Flowers face-plant into the mud after rain|Double varieties are top-heavy by design — install peony rings early, or grow single/Itoh types that self-support
Buds form then dry up|Small buds that never open|Late frost, drought during bud-up, or root competition — water in dry springs and check what's stealing moisture",
                Faqs: @"How long until a new peony blooms?|Bare roots usually give a token bloom in year two and hit stride by year four. Patience is the price of a 50-year plant.
Do peonies really live 50 years?|Yes — undisturbed clumps flowering for half a century are well documented. They routinely outlive the gardener.
Can I move an established peony?|Only if you must, and only in early autumn: take the whole root mass, keep eyes shallow, and accept a couple of quiet years after.
Do peonies grow in warm climates?|They need winter chill. In mild-winter regions choose early-flowering singles or Itoh hybrids and give afternoon shade.",
                Gear: @"Peony support rings|Installed early, invisible by May, essential by June
Compost|One spring mulch is the entire feeding programme
Sharp spade|For the once-a-decade divide, clean cuts heal best
Fridge space|The florist's trick for peonies on demand"),

            ["Moth Orchid"] = new(
                Sections: @"
## Light & placement
The moth orchid is the easiest orchid ever domesticated — most die from too much love, not neglect. It lives on tree branches in the wild, so think dappled brightness, never direct sun.
- An east-facing windowsill is perfect; south or west needs a sheer curtain
- Leaves tell the truth: grass-green means happy; very dark green means too little light; red-tinged or bleached means too much
- Ordinary room temperature (18–27 °C) suits it, away from radiators and cold draughts

## Watering — where orchids are won and lost
Those silvery roots in the clear pot aren't in soil — they're in bark, and they want a rainstorm-then-dry cycle, not constant damp.
- Once a week, stand the pot in room-temperature water for 15 minutes, then drain completely
- Water the bark, not the crown — water pooled where the leaves meet rots the plant
- Roots are your gauge: silvery = thirsty, bright green = fine, brown mush = drowning
WARN: Never let the pot sit in a saucer of water. Root rot from standing water is the #1 moth-orchid killer.

## Feeding & repotting
- Feed 'weakly, weekly': quarter-strength orchid feed most waterings in spring and summer, plain water in winter
- Repot every 1–2 years when bark breaks down, in the same size clear pot with fresh orchid bark — never potting soil
- Green roots wandering out of the pot into the air are healthy, not a problem; leave them

## The rebloom trick
The reason most people bin a perfectly good orchid: the flowers drop and nothing happens. Rebooting one is genuinely easy.
- When the last flower falls, cut the spike above the second node (the small bumps on the stem) — often a side branch of new buds appears
- If the spike browns entirely, cut it at the base; the plant will make a fresh one
- The trigger is a temperature drop: 3–4 weeks of cooler nights (around 16 °C — an autumn windowsill does it) reliably starts a new spike
TIP: A healthy moth orchid blooms for 2–4 months at a stretch and can flower twice a year for decades.",
                Calendar: @"Jan|Water fortnightly if cool; keep off cold windowsills at night
Feb|Peak indoor bloom season — enjoy
Mar|Resume weekly watering and weak feeding
Apr|Repot if bark is old, once flowering ends
May|Move away from strengthening direct sun
Jun|Weekly soak; watch silvery roots as your gauge
Jul|Warm-weather growth — new leaves and air roots appear
Aug|Keep feeding weakly; wipe dust off leaves
Sep|Cooler nights by the window trigger next spike
Oct|Watch for a new flower spike (mistaken often for a root — spikes have a mitten tip)
Nov|Stake the developing spike loosely as it rises
Dec|Buds open; ease off feeding until spring",
                Problems: @"Wrinkled, leathery leaves|Limp leaves with accordion wrinkles|The roots aren't delivering water — either bone-dry (soak it) or rotted from overwatering (repot, trim mush, water less)
Root rot|Brown, hollow, mushy roots; plant wobbles in the pot|Unpot, snip all dead roots with clean scissors, repot in fresh bark, and switch to the soak-and-drain routine
Buds drop before opening|Fat buds yellow and fall|Bud blast — caused by a sudden change: cold draught, dry radiator air, or a house move. Stabilise its spot; the next spike will hold
Sticky drops on leaves|Clear sticky sap, maybe tiny brown discs|Sap alone can be natural; discs are scale insects — wipe off with cotton buds dipped in soapy water weekly until clear",
                Faqs: @"How long do the flowers last?|Two to four months is normal — the longest-blooming houseplant flower there is.
Do I really water with ice cubes?|Skip the gimmick: melting ice chills tropical roots. A proper 15-minute soak-and-drain is just as easy and far better.
Why are roots growing out of the pot into the air?|Completely normal — it's an air plant by nature. Aerial roots are a health sign, not an escape attempt.
Is it toxic to cats?|No — moth orchids are non-toxic to cats and dogs, one of the safest flowering houseplants.",
                Gear: @"Clear orchid pot with slits|Lets you read root colour and lets roots breathe
Orchid bark mix|Potting soil suffocates orchid roots — bark never does
Quarter-strength orchid feed|'Weakly, weekly' is the whole programme
Small stake and clips|A rising flower spike appreciates gentle guidance"),

            ["Dahlia"] = new(
                Sections: @"
## Soil & site
Dahlias are the hardest-working flowers in the garden — blooming non-stop from midsummer to the first frost — but they're tender, greedy, and thirsty. Feed the soil and they'll repay you tenfold.
- Full sun: eight hours for the best flower count
- Rich, moisture-retentive soil with plenty of compost dug in
- Shelter from strong wind; the big-flowered types are sails on sticks

## Planting the tubers
Tubers go out only when frost is finished — they're as tender as tomatoes.
- Start tubers in pots indoors in early spring for a head start, or plant direct after last frost
- Plant 10–15 cm deep, the old stem stub pointing up, 60 cm apart
- Put the support stake in at planting time — doing it later spears the tuber
WARN: Don't water a freshly planted tuber until shoots appear — dormant tubers in cold wet soil rot before they wake.

## The pinch, and growing on
One 10-second act doubles your flowers: when the plant has 3–4 sets of leaves, pinch out the main growing tip. It branches into a bushy plant with far more blooms.
- Water deeply 2–3 times a week once growing strongly
- Feed fortnightly with high-potash (tomato) fertiliser from July
- Tie stems to the stake as they climb
TIP: For dinner-plate blooms, do the opposite of pinching — disbud: remove the two side buds beside each central bud, and the plant pours everything into one giant flower.

## Deadhead like you mean it
A dahlia deadheaded every few days blooms until frost; one left to seed shuts down in weeks.
- Spent heads are cone-shaped, new buds are round — cut the cones down to a side shoot
- Cutting armfuls for the vase counts as deadheading; the more you cut, the more it makes

## Frost and the winter question
The first frost blackens dahlias overnight — that's the signal, not a disaster.
- Cut stems to 15 cm after frost blackens them
- Cold regions: lift tubers, dry upside-down for two weeks, store in barely-damp compost somewhere frost-free, checking monthly for rot
- Mild regions (rarely below −5 °C): leave in the ground under a thick 15 cm dry mulch and skip the fuss",
                Calendar: @"Jan|Check stored tubers for rot; cut away any soft spots
Feb|Order new varieties before the good ones sell out
Mar|Start tubers in pots indoors in a bright frost-free place
Apr|Pot on; take basal cuttings from strong shoots for free plants
May|Plant out after last frost, stake at planting
Jun|Pinch tips at 3–4 leaf pairs; begin watering deeply
Jul|First blooms; start fortnightly tomato feed
Aug|Peak season — deadhead every few days, cut for the vase
Sep|Blooms keep coming; keep feeding and tying in
Oct|Enjoy until the first frost blackens the plants
Nov|Lift, dry, and store tubers (or mulch heavily in mild areas)
Dec|Store cool and frost-free; rest — the dahlia is",
                Problems: @"Earwigs in the blooms|Petals nibbled ragged overnight|The classic trap works: upturned pots stuffed with straw on canes among the plants — empty them each morning far away
Slugs on young shoots|Emerging shoots eaten to ground level in spring|Protect plants until 30 cm tall — beer traps, wildlife-safe pellets, or nightly patrols; young dahlias are slug caviar
Powdery mildew|White dust on leaves from late summer|Water the soil not the foliage, keep plants well spaced; late-season mildew looks bad but barely dents flowering
Tubers rot in storage|Soft, smelly patches by midwinter|Dry tubers thoroughly before storing, keep storage barely damp not wet, check monthly and cut rot out promptly to save the rest",
                Faqs: @"Do I have to lift the tubers?|Only where the ground freezes deeply. Many gardeners in mild regions leave them under a dry mulch for years — the risk is a wet, not just cold, winter.
Why is my dahlia all leaves and no flowers?|Too much nitrogen and not enough sun are the usual culprits — switch to tomato feed and remember they want 8 hours of light.
Can I grow dahlias from seed?|Yes — sown warm in spring they flower the same summer. Seed gives single, bee-friendly blooms; named show varieties need tubers or cuttings.
When do I plant out?|The tomato rule: whenever it's safe to plant tomatoes outside where you live, it's safe for dahlias.",
                Gear: @"Sturdy stakes and soft twine|In before the plant needs them, always
Tomato fertiliser|High-potash fuel for non-stop flowering
Sharp snips|Deadheading is a daily pleasure with good tools
Crates and dry compost|Winter quarters for lifted tubers
Upturned pots and straw|The 300-year-old earwig trap that still wins"),

            ["Hydrangea"] = new(
                Sections: @"
## Soil & site
'Hydrangea' begins with water, and the name is a warning. Give it morning sun, afternoon shade, and soil that never fully dries, and it will bloom like nothing else in shade-touched gardens.
- Morning sun with afternoon shade is ideal; deep all-day shade means few flowers, full afternoon sun means daily wilting
- Rich, moisture-retentive soil with compost dug in generously
- Shelter from cold east winds protects the flower buds of mophead types

## The colour magic
Mophead and lacecap hydrangeas are living pH meters — the same plant flowers blue in acid soil and pink in alkaline.
- Acid soil (pH below 6): aluminium is available, flowers turn blue
- Alkaline soil (pH above 7): aluminium locks up, flowers turn pink
- To go blue: work in sulphur or use a 'hydrangea blueing' aluminium feed, and water with rainwater — tap water is often limy
- To go pink: add garden lime each spring
TIP: White varieties stay white whatever you do — their colour isn't pH-driven. And in pots, colour control is far easier than in the ground.

## Watering & feeding
The first wilt you see is a request, not an emergency — but chronic thirst costs you next year's flowers.
- Deep soak once or twice a week in summer; pots may need water daily in heat
- Mulch 5–7 cm deep every spring to lock moisture in
- One spring feed of balanced slow-release fertiliser is enough; overfeeding gives leaves at the expense of blooms

## Pruning — know your type first
More hydrangea flowers are lost to well-meaning pruning than to any pest. The rule that matters: know where your variety flowers.
- Mopheads & lacecaps (macrophylla) flower on OLD wood: prune lightly only — remove spent heads down to the first fat bud pair in spring, plus a few oldest stems at the base
- Panicle (paniculata) & smooth (arborescens) types flower on NEW wood: these you can cut back hard by half or more each spring, and they bloom bigger for it
WARN: Cut a mophead to the ground in spring and you've removed every flower for the year — it will look healthy and bloom zero. When in doubt, prune less.
- Leave the faded flowerheads on all winter: they're frost protection for next year's buds (and beautiful rimed in frost); remove them in spring

## Hydrangeas in a vase
- Cut mature blooms (papery, not fresh-soft) — young heads wilt within hours
- Sear stem ends in boiling water for 30 seconds, then deep cold water
- Whole heads dry beautifully: stand stems in 3 cm of water and let it evaporate",
                Calendar: @"Jan|Leave the frosted flowerheads on — they're bud armour
Feb|Order new varieties; plan pH experiments
Mar|Prune by type: light for mopheads, hard for panicles
Apr|Feed once, mulch deep, apply blueing agent if going blue
May|Leaf-out; water as buds develop
Jun|First mophead blooms; deep-soak weekly
Jul|Peak bloom; pots may need daily water
Aug|Blooms mature to papery — cut for drying
Sep|Panicle types glow pink as nights cool
Oct|Colour deepens; stop feeding, keep watering until rain returns
Nov|Resist tidying — heads stay on for winter
Dec|Admire the frost-rimmed skeletons",
                Problems: @"No flowers at all|Healthy leafy shrub, no blooms for a year|Almost always pruning old-wood types hard in spring, or a late frost killing buds — prune lightly and protect from east winds; flowers return next year
Daily dramatic wilting|Whole plant collapses each afternoon, recovers by night|Too much afternoon sun or too little water — mulch deeply, soak more, or move it (autumn) somewhere with afternoon shade
Flowers went muddy mauve|Blue turned pinkish-grey mid-season|Soil pH is drifting neutral — recharge with aluminium sulphate and switch to rainwater
Leaf scorch|Brown crispy leaf edges in high summer|Sun and wind outpacing the roots — water deeply, mulch, and accept some scorch in heatwaves; it's cosmetic",
                Faqs: @"Why does the same plant bloom two colours?|Pockets of different pH around the roots — charming, and normal in soil near paths or mortar where lime leaches in.
Can I change white hydrangeas blue?|No — white varieties don't respond to pH. Colour-shifting only works on pink/blue mopheads and lacecaps.
Do hydrangeas grow in pots?|Very well: compact varieties, a 45 cm+ pot, ericaceous compost if you want blue, and generous watering.
Are hydrangeas toxic?|Mildly — leaves and buds contain compounds that upset pets' stomachs if eaten in quantity. Serious cases are rare.",
                Gear: @"Mulch, lots of it|The difference between weekly and daily watering
Aluminium sulphate ('blueing' feed)|The blue switch, for acid-leaning soil
Garden lime|The pink switch
Rainwater butt|Tap water slowly un-blues blue hydrangeas
Sharp secateurs|For spring pruning — by type, never by habit")
        };
    }
}
