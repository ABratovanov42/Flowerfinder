namespace Flowerfinder.Data
{
    // Step-by-step growing timelines, keyed by CommonName.
    // Format per line:  timeframe|step title|what to do
    // Rendered as the "How to grow it, step by step" vine on detail pages.
    public static class GrowPlans
    {
        public static readonly Dictionary<string, string> All = new()
        {
            ["Rose"] = """
Day 1|Plant the rose|Dig a hole twice as wide as the root ball. Set the plant so the graft point (the knobbly join) sits just above soil level, backfill, and water in deeply.
Weeks 1-3|Settle it in|Water deeply twice a week at the base — never over the leaves. Add a 5 cm mulch ring, keeping it clear of the stem.
Weeks 4-8|First feeding|Once new shoots appear, feed with rose fertiliser and keep watering weekly. Watch for aphids on soft new growth; blast them off with water.
Months 3-5|First blooms|Deadhead spent flowers by cutting back to the first leaf with five leaflets — that's where the next bloom comes from.
Every spring|The big prune|In early spring cut the whole plant back by a third to a half, removing dead or crossing stems, always cutting just above an outward-facing bud.
""",
            ["Tulip"] = """
Day 1 (autumn)|Plant the bulbs|In October or November, plant bulbs pointy end up, 15 cm deep and 10 cm apart, in free-draining soil. Water once, then leave alone.
Winter|Do nothing|The bulb needs 12-14 weeks of cold to trigger flowering. Resist watering — soggy winter soil rots bulbs.
Early spring|Shoots emerge|When green tips push through, water only in dry spells. Protect emerging shoots from slugs.
Weeks 2-3 of spring|Bloom|Enjoy. Cut a few for the vase early in the morning — tulips keep growing in water.
After flowering|Feed the bulb|Snap off the spent flower head but leave all foliage six weeks until it yellows — it's recharging the bulb for next year.
""",
            ["Sunflower"] = """
Day 1|Sow after frost|Push seeds 2.5 cm deep and 30 cm apart directly where they'll grow, in the sunniest spot you have. Water well.
Days 7-10|Germination|Keep the soil evenly moist. Protect the emerging seedlings from slugs and birds — a cut-off plastic bottle over each works.
Weeks 2-5|Rapid growth|Water deeply twice a week. When plants reach 60 cm, push in a tall stake and tie loosely — summer storms topple unstaked sunflowers.
Weeks 6-10|The climb|Feed fortnightly with liquid fertiliser. Tall varieties can grow 5 cm a day now.
Weeks 10-14|Bloom|The head tracks the sun while in bud, then settles facing east. Leave spent heads standing — birds feast on the seeds all autumn.
""",
            ["Lavender"] = """
Day 1 (spring)|Plant lean|Choose the sunniest, best-drained spot you have. Mix grit into the planting hole, set the plant high, and water in once. No compost, no fertiliser — lavender likes it poor.
Weeks 1-4|Establish|Water once a week for the first month only. After that, rain is usually enough — overwatering is the number-one lavender killer.
Summer|First bloom|Bees arrive. Harvest stems for drying just as the bottom flowers open — scent is strongest then.
After flowering|Trim|Shear the whole plant back by a third, into leafy growth but never into bare wood — old wood won't resprout.
Every year|Repeat the trim|An annual post-bloom haircut keeps lavender dense and fragrant for 10-15 years instead of woody and sparse in 4.
""",
            ["Peony"] = """
Day 1 (autumn)|Plant shallow|This is the step everyone gets wrong: set the root so its buds ('eyes') sit no more than 5 cm below the surface. Too deep = leaves but no flowers, ever.
Year 1|Be patient|Water in dry spells and remove any flower buds that form — let the plant put everything into roots. It feels cruel; it works.
Year 2, spring|First real blooms|Push in a peony ring or stakes early — the huge flowers flatten in rain without support. Feed once with a low-nitrogen fertiliser.
Each summer|After the show|Deadhead spent blooms but leave the foliage all summer. Cut everything to the ground in late autumn and bin (don't compost) the leaves.
For decades|Leave it alone|Peonies hate being moved and can bloom in the same spot for 50 years. Don't divide unless flowering declines.
""",
            ["Moth Orchid"] = """
Day 1|Pot in bark|Never soil. Use orchid bark in a clear pot with drainage holes — the roots photosynthesise and like light. Set it in bright, indirect light.
Weekly|Water by soaking|Dunk the whole pot in room-temperature water for 10 minutes, then let it drain completely. Water only when the roots look silvery, not green.
Monthly|Feed lightly|Add orchid feed at half strength to the soak, three weeks out of four.
After flowering|Trigger a rebloom|Cut the spent spike just above the second node from the base. A few cooler nights (around 16°C) often trigger a new spike.
Every 2 years|Repot|When roots crowd the pot or bark breaks down, repot into fresh bark just after flowering. Trim any brown, hollow roots first.
""",
            ["Common Daisy"] = """
Day 1 (late summer)|Sow|Scatter seed thinly over moist compost or a bare lawn patch and press in — don't bury; daisy seed needs light to germinate.
Weeks 1-2|Germination|Keep moist. Thin seedlings to 15 cm apart once they have two true leaves.
Autumn|Plant out|Move young plants to their flowering spot. They'll sit as low rosettes through winter — that's normal.
Spring|Bloom|Flowers open with the sun and close at dusk, week after week. Deadhead to keep them coming.
Ongoing|Let them wander|Daisies self-seed gently into lawns. Mow around clumps if you want next year's flowers for free.
""",
            ["Dahlia"] = """
Day 1 (after last frost)|Plant the tuber|Dig a 15 cm deep hole, lay the tuber horizontally with the old stem stump upward, cover, and DON'T water yet — tubers rot in cold wet soil before they sprout.
Weeks 2-4|First shoots|Start watering once shoots appear. Put slug protection down immediately — slugs adore dahlia shoots.
At 40 cm tall|Pinch|Snip out the main growing tip above the third pair of leaves. This one cut doubles the number of flowering stems.
Summer|Feed and stake|Stake tall varieties, water deeply twice a week, and feed fortnightly with tomato fertiliser once buds form.
Until first frost|Cut, cut, cut|The more you cut for the vase, the more it blooms. After frost blackens the foliage, either mulch thickly or lift and store the tubers dry indoors.
""",
            ["Bearded Iris"] = """
Day 1 (mid-late summer)|Plant on the surface|Plant rhizomes almost sitting on the soil, top exposed to the sun, facing the light. Burying them is the classic mistake — they need to bake.
Weeks 1-4|Root in|Water twice a week until new leaf growth shows, then stop pampering.
Spring|Bloom|Each stem opens several flowers in sequence. Snap off each bloom as it fades to keep the stem flowering.
After flowering|Tidy|Cut the flower stem to the base. Trim leaf fans by half in late summer if they look scruffy.
Every 3-4 years|Divide|When the clump goes bald in the middle, lift it after flowering, cut healthy young rhizome sections with a leaf fan each, and replant those.
""",
            ["Marigold"] = """
Day 1|Sow anywhere sunny|Direct-sow 5 mm deep after frost, or start indoors a month earlier. Seeds are big and germinate almost embarrassingly fast.
Days 4-7|Germination|Keep just moist. Thin or pot on when seedlings have two true leaves.
Week 4|Pinch|Nip out the growing tip once plants are 15 cm tall for bushy plants covered in buds.
Weeks 6-16|Bloom machine|Deadhead every few days and marigolds flower without pause until frost. Water at the base; the dense flowers rot if soaked.
Ongoing|Recruit them|Plant spares beside tomatoes and beans — their roots suppress soil pests. Save dried seed heads for next year.
""",
            ["Daffodil"] = """
Day 1 (early autumn)|Plant deep|Plant bulbs at twice their own depth (usually 10-15 cm), pointy end up, in drifts rather than rows for a natural look.
Winter|Wait|Nothing to do. The bulbs are rooting quietly below ground.
Late winter|First up|Daffodils are often the first sign of spring — shoots shrug off snow. No protection needed.
Spring|Bloom|Deadhead spent flowers so energy goes to the bulb, not seed.
6 weeks after bloom|The golden rule|Leave the foliage standing — don't cut, don't knot it — until it yellows naturally. This feeds next spring's flowers.
""",
            ["Hyacinth"] = """
Day 1 (autumn)|Plant|Set bulbs 10 cm deep and 8 cm apart in well-drained soil, or one bulb per pot for indoor forcing. Wear gloves — the bulbs irritate some skin.
For indoor bloom|Chill first|Pot the bulb, then keep it dark and cold (4-9°C) for 10 weeks — a shed or fridge drawer works. Skipping the chill gives leaves but no flower.
Weeks 11-14|Bring to light|Move to a cool bright room. The spike rises fast, and one stem can perfume the whole house.
Bloom|Support if needed|Heavy heads may flop — a discreet cane helps. Cooler rooms make flowers last weeks longer.
After flowering|Garden retirement|Plant forced bulbs outside. They'll return each spring — smaller and looser, but reliably fragrant.
""",
            ["Chrysanthemum"] = """
Day 1 (spring)|Plant|Set young plants 45 cm apart in fertile, well-drained soil, in full sun.
Until midsummer|Pinch relentlessly|Every time a shoot reaches 15 cm, pinch out its tip. Stop at midsummer. This is the whole secret to a dome of hundreds of flowers.
Summer|Feed|Water at the base and feed fortnightly. Good airflow between plants prevents mildew.
Autumn|The main event|Mums peak as everything else fades, shrugging off light frosts. Deadhead to extend the show to a month or more.
After frost|Cut down and mulch|Cut stems to 15 cm and mulch the crown. In cold regions, lift a plant or two into a cold frame as insurance.
""",
            ["Camellia"] = """
Day 1 (autumn)|Plant with care|Choose a sheltered spot with no morning sun (it scorches frosted buds) and acidic soil. Plant with the root ball's top level with the soil — never deeper.
Months 1-6|Water and mulch|Water generously with rainwater (tap water is often too alkaline) and mulch 7 cm deep with bark or leaf mould.
Summer|The invisible season|Keep watering — this is when next winter's flower buds form. A dry August means no flowers in February.
Winter-spring|Bloom|Flowers open when little else dares. Snap off blooms as they brown.
Ongoing|Prune rarely|Only trim immediately after flowering if shape demands. Feed with ericaceous fertiliser in spring, and never lime the soil.
""",
            ["Magnolia"] = """
Day 1|Choose forever|Pick the spot like you're planting a monument — magnolias hate being moved. Sheltered from north winds, room for 50 years of growth, deep slightly acidic soil.
Year 1|Water faithfully|A young magnolia needs a deep weekly soak through its first two growing seasons. Mulch generously, keeping it off the trunk.
Years 2-4|Hands off|Don't prune, don't fertilise heavily, don't dig around the shallow fleshy roots. Just water in droughts.
Each spring|The show|Flowers open on bare wood before the leaves — one hard frost can brown them overnight, which is why shelter mattered on day 1.
Decades|Reward|A mature magnolia in full bloom is the best return on patience in gardening.
""",
            ["Jasmine"] = """
Day 1 (spring)|Plant by a support|Plant at the foot of a warm, sunny wall with wires or a trellis. Water in well and tie the first shoots to the support.
Weeks 2-8|Train|Tie in new growth weekly — jasmine twines fast once it takes hold. Water twice weekly in the first summer.
Summer|First perfume|Flowers open white and pour out scent from dusk onward. Plant it near a window or seat you use in the evening.
After flowering|Prune|Cut flowered stems back to a strong side shoot. Jasmine blooms on last year's wood, so late pruning costs next year's flowers.
Each spring|Feed and refresh|Feed with a high-potash fertiliser and mulch. Thin a third of the oldest stems on mature plants to keep flowers coming low down.
""",
            ["Cosmos"] = """
Day 1 (after frost)|Sow where it flowers|Scatter seed on raked soil and cover 5 mm. Poor soil is genuinely better — rich soil gives ferny leaves and no flowers.
Days 7-10|Germination|Keep moist until seedlings show, then ease off. Thin to 30 cm apart.
Week 5|Pinch|Pinch the central tip once plants are 20 cm tall. Sturdier plants, triple the flowers.
Weeks 8-24|Non-stop bloom|Deadhead every few days — cosmos flowers until the first frost if you never let it set seed. No feeding needed, water only in drought.
Autumn|Save seed|Let a few last heads mature and dry. One envelope of saved seed refills the border next year for free.
""",
            ["Zinnia"] = """
Day 1 (warm soil)|Sow direct|Zinnias sulk if transplanted, so sow 5 mm deep straight into warm soil in full sun, 25 cm apart.
Days 5-8|Germination|Fast and satisfying. Keep moist but never soggy.
Week 4|Pinch for bounty|Snip the main tip above two pairs of leaves. Every cut stem afterwards triggers two more.
Summer|Cut and come again|Water at the base only — wet leaves invite mildew. Cut flowers constantly; the vase is pruning.
Until frost|Keep picking|Zinnias out-bloom nearly everything if deadheaded. Save seed from the best plants at season's end.
""",
            ["Madonna Lily"] = """
Day 1 (late summer)|Plant shallow|Unlike other lilies, plant this bulb barely 2-3 cm deep in well-drained, slightly alkaline soil in full sun.
Autumn|Winter rosette|A low tuft of leaves appears and stays green all winter. Don't panic and don't cut it — it's meant to be there.
Spring|The spike|A stem rises to 1.2 m or more. Stake early in windy gardens; watch for scarlet lily beetles and pick them off.
Early summer|Bloom|Pure white trumpets with golden pollen, and evening perfume that carries across the garden.
After flowering|Minimal fuss|Cut the stem down when yellow. Leave the bulb undisturbed — it resents moving and improves with age.
""",
            ["Pansy"] = """
Day 1 (late summer)|Sow cool|Sow 3 mm deep in trays somewhere cool and shaded — pansy seed refuses to germinate in heat. Darkness helps too.
Weeks 2-3|Germination|Move seedlings to bright light. Pot on when two true leaves show.
Autumn|Plant out|Set plants 20 cm apart. They flower through mild winter spells and explode in spring.
All season|Deadhead ruthlessly|A pansy allowed to set seed stops flowering. Two minutes of deadheading a week keeps faces coming for months.
Summer heat|Graceful exit|Pansies collapse in heat. Compost them without guilt and sow the next batch in late summer.
""",
            ["Hydrangea"] = """
Day 1 (spring or autumn)|Plant in morning shade|Choose dappled or afternoon-shade spots with rich, moist soil. Water in with a full can — the name literally means 'water vessel'.
Weeks 1-8|Never let it dry|Deep-water twice a week in the first season. A wilting hydrangea recovers, but repeated wilting weakens it for years.
Want blue flowers?|Check your soil|In acidic soil, mophead blooms turn blue; in alkaline, pink. Aluminium sulphate nudges blue; lime nudges pink. Whites stay white regardless.
Summer|Bloom|Enormous heads for weeks. Water is the only feeding it really needs.
Late winter|Prune lightly|Leave the faded heads on all winter (they protect the buds and look lovely frosted), then cut back to the first fat pair of buds. Never hard-prune moptops — you'll cut off the year's flowers.
""",
            ["Petunia"] = """
Day 1 (10 weeks before frost ends)|Sow on the surface|Petunia seed is dust-fine and needs light: press onto moist compost, don't cover, and keep at 21°C under a clear lid.
Weeks 2-8|Grow on|Pot on tiny seedlings carefully. Give maximum light so they stay stocky, and pinch tips once at 10 cm.
After last frost|Plant out|Baskets, pots, borders — 25 cm apart in sun. Buying young plants instead of sowing is honestly fine too.
All summer|Feed weekly|Petunias in containers are hungry: liquid feed weekly, water daily in heat, and deadhead including the seed pod behind the flower.
Midsummer|The rescue cut|When plants go leggy, cut the whole thing back by half. It looks brutal, but ten days later it's a fresh mound of bloom until frost.
""",
            ["Begonia"] = """
Day 1 (early spring)|Start tubers indoors|Press tubers hollow-side-up into trays of moist compost, barely covered, at 18°C in good light.
Weeks 2-6|Shoots|Pot up when shoots reach 5 cm. Water around, not on, the tuber.
After last frost|Plant out in shade|Begonias thrive where sun-lovers sulk — bright shade, rich soil, 25 cm apart. Superb in pots on shaded patios.
All summer|Steady moisture|Water when the surface dries and feed fortnightly. Remove the small single female flowers behind the big doubles for maximum show.
Autumn|Store the tubers|Before hard frost, lift tubers, dry them for two weeks, and store in paper bags of dry compost indoors. They come back bigger every year.
""",
            ["Snapdragon"] = """
Day 1 (late winter)|Sow on the surface|Sprinkle the tiny seed on moist compost, don't cover (it needs light), and keep at 18°C on a bright windowsill.
Weeks 2-3|Germination|Grow on cool — snapdragons like it fresher than most bedding. Pot on at two true leaves.
Week 8|Pinch|Pinch the tip at 10 cm for four times the flower spikes.
After light frosts pass|Plant out|Snapdragons shrug off a light frost. Set 25 cm apart in sun. Squeeze a flower's jaws — every child should see the dragon open.
Summer-autumn|Keep spikes coming|Cut or deadhead spent spikes to the next side shoot. Plants often overwinter mild years and bloom again bigger.
""",
            ["Sweet Pea"] = """
Day 1 (autumn or late winter)|Sow deep in pots|Sow 2 cm deep in tall pots or root trainers — sweet peas grow a taproot. Cool and bright suits them; no heat needed.
At 10 cm|Pinch|Nip the tip above two pairs of leaves. Side shoots carry the flowers.
Early spring|Plant at a wigwam|Set out 20 cm apart at canes or netting with compost-enriched soil. Tie in the first stems; tendrils do the rest.
From first flower|Pick every 2-3 days|This is the deal sweet peas offer: cut constantly and they bloom for months; let one pod form and they quit. Scent fills the house.
All summer|Water and mulch|Never let the roots dry, feed weekly with high-potash food, and keep picking.
""",
            ["Carnation"] = """
Day 1 (spring)|Plant in the sun|Set plants 30 cm apart in well-drained, slightly alkaline soil. Grey-blue foliage means it wants sun and hates wet feet.
Weeks 2-6|Establish|Water moderately. Push in short twiggy sticks — stems get top-heavy in flower.
Buds appear|Disbud for size|For big single blooms, remove the small side buds below the main one. For sprays, leave them all.
Summer|Bloom|Clove-scented flowers over weeks. Deadhead to the next bud down the stem.
After flowering|Take cuttings|Snap out 8 cm side shoots, strip lower leaves, and root them in gritty compost — free plants that stay young as the parent gets woody.
""",
            ["Gerbera"] = """
Day 1|Plant proud|In pots or a sunny bed, set the crown slightly above soil level — water pooling in the crown is the main killer.
Weeks 1-4|Settle in|Water when the top of the compost dries, always from below or at the edge, never into the leaf rosette.
Bud time|Feed|High-potash liquid feed fortnightly brings up those perfect daisy wheels on long stems.
Bloom|Cut, don't snip|Harvest flowers by pulling the whole stem from the base with a twist — it triggers replacements faster than cutting.
Winter|Keep it dryish|In cold climates, bring pots to a bright frost-free porch and water sparingly until spring.
""",
            ["Ranunculus"] = """
Day 1 (autumn, mild areas)|Soak the claws|Soak the odd little octopus corms for 3-4 hours (no longer) until plump, then plant claws-down 5 cm deep, 10 cm apart.
Weeks 2-4|Sprout cool|They root and leaf up in cool weather. In cold regions, start in trays in a frost-free frame instead and plant out in spring.
Late winter|Growth|Water when dry, keep slugs off the lush foliage.
Spring|Rose-like bloom|Layer upon layer of petals, superb for cutting — take stems when buds show colour but aren't open.
After bloom|Dry off|Let foliage die down, then lift corms and store dry, or leave in the ground only where summers are dry and winters mild.
""",
            ["Freesia"] = """
Day 1 (spring)|Plant corms|Set corms pointy end up, 5 cm deep and 8 cm apart, in pots or a sunny, sheltered bed.
Weeks 3-6|Grass-like shoots|Water moderately. The thin leaves flop — a few twiggy sticks keep things tidy.
Buds|Support the spray|Wiry stems carry one-sided sprays of buds. Feed fortnightly once buds show.
Bloom|The scent|One of the great flower perfumes. Cut stems when the first bud opens; the rest open in the vase.
After flowering|Rest|Let the foliage yellow, then store corms dry indoors over winter for replanting in spring.
""",
            ["Gladiolus"] = """
Day 1 (spring)|Plant in batches|Plant corms 10-15 cm deep (deep = self-supporting), 15 cm apart. Plant a new batch every two weeks until midsummer for months of continuous spikes.
Weeks 2-6|Swords rise|The leaf blades explain the name (gladius = sword). Water weekly and keep weeded.
Weeks 10-12|Spikes|Stake in windy spots. Cut for the vase when the bottom two florets show colour — leave at least four leaves to feed the corm.
Bloom|Bottom-up|Florets open in sequence up the spike over a week or more.
Autumn|Lift and store|After first frost, lift corms, dry two weeks, snap off the old spent corm underneath, and store the new ones cool and dry.
""",
            ["Crocus"] = """
Day 1 (early autumn)|Plant a hundred|Crocuses only read as drifts. Plant corms 8 cm deep, pointy end up, scattered by the handful and planted where they land — lawns, borders, under trees.
Winter|Nothing|They're rooting. Squirrels are the only threat — chicken wire over new plantings helps.
Late winter|First colour|Flowers open wide on sunny days and close in cloud. The year's first bees arrive within hours.
After bloom|Six-week rule|If they're in the lawn, don't mow for six weeks after flowering so the leaves can recharge the corms.
Every year|Multiplication|Left alone, colonies double every few years. The best effort-to-joy ratio in bulbs.
""",
            ["Snowdrop"] = """
Day 1 (spring, 'in the green')|Plant just after flowering|Dry snowdrop bulbs establish poorly — buy or beg clumps 'in the green' (leaves still on) and replant immediately at the same depth, 8 cm apart, in dappled shade.
First winter|The wait|Moist soil under deciduous trees is their happy place. No care needed.
Midwinter|Bloom|Flowers push through frozen ground and even snow — the first flower of the year, weeks before anything else dares.
Spring|Die back|Let foliage fade naturally.
Every 3 years|Divide after flowering|Lift a congested clump while leaves are green, split into threes and fours, replant at once. Colonies spread into white carpets over the years.
""",
            ["Bluebell"] = """
Day 1 (autumn)|Plant deep in shade|Plant bulbs 10 cm deep under deciduous trees or a shady border — anywhere that gets spring light before the leaf canopy closes.
Winter|Root quietly|Nothing to do. Make sure you planted the English species (nodding, one-sided, scented) rather than the upright Spanish invader.
Mid-spring|The haze|Flowers arch and nod in that unmistakable violet-blue, scenting whole woodlands.
After bloom|Leave everything|Foliage feeds the bulb; seed pods feed the colony. Don't tidy.
Years|A carpet|Bluebells naturalise slowly but permanently. Never dig them from the wild — buy cultivated stock.
""",
            ["Poppy"] = """
Day 1 (autumn or early spring)|Scatter, don't bury|Mix the dust-fine seed with sand and scatter over raked, poor soil. Tread lightly. Light triggers germination — covering it is the classic failure.
Weeks 2-4|Seedlings|Thin ruthlessly to 20 cm apart. Poppies transplant badly, so thin rather than move.
Late spring|Buds|The nodding buds swing upright the day before opening.
Summer|Four-day flowers|Each silk-petalled bloom lasts days, but new buds keep coming for weeks. Deadhead for more, or...
Late summer|...let the pepperpots ripen|Leave pods to dry and shake seed where you want next year's reds. Disturbing the soil each autumn keeps the show going — poppy seed waits decades for its chance.
""",
            ["Cornflower"] = """
Day 1 (autumn or spring)|Sow direct|Sow 1 cm deep in patches in full sun and poor-to-average soil. Autumn sowings make bigger, earlier plants.
Weeks 2-3|Germination|Thin to 20 cm. Push in twiggy sticks early — the wiry stems appreciate them.
Early summer|That blue|A blue almost nothing else in the garden can match. Bees and hoverflies think so too.
All summer|Cut and deadhead|Cut for the vase freely and deadhead weekly; plants respond with months of flowers.
Autumn|Self-sowing|Let the last flowers seed. Cornflowers happily return each year where the soil is open.
""",
            ["Foxglove"] = """
Day 1 (early summer)|Sow for next year|Foxgloves are biennial: sow this June, flower next June. Press the fine seed onto trays of compost — light needed, don't cover.
Weeks 2-4|Prick out|Move seedlings to individual pots when tiny; plant out in autumn, 45 cm apart, in dappled shade.
Winter|The rosette|A handsome low rosette sits through winter. That's the plan, not a problem.
Next summer|The spire|A 1.5 m spike of speckled bells, opening bottom-up over weeks. Bumblebees disappear inside each glove. (All parts are toxic — site away from grazing children and pets.)
After bloom|Succession|Cut the main spike after flowering for smaller side spikes, then let one plant shed its thousands of seeds — you'll never need to sow again.
""",
            ["Delphinium"] = """
Day 1 (spring)|Plant into luxury|Delphiniums are the aristocrats: deep, rich soil, full sun, shelter from wind, 50 cm spacing. Enrich the hole with compost.
Immediately|Declare war on slugs|Slugs can erase a delphinium overnight. Protect new shoots from day one, every spring.
At 30 cm|Thin and stake|Reduce each clump to 5-7 strongest shoots and stake every one — the spikes reach 1.8 m and snap in wind.
Early summer|The spires|Unmatched columns of blue. Feed weekly once spikes form.
After first flush|Cut hard for round two|Cut spent spikes to the ground, feed and water generously, and most years you get a second, smaller bloom in September.
""",
            ["Lupine"] = """
Day 1|Nick and soak|Nick each hard seed with a nail file and soak overnight — germination jumps from patchy to near-total.
Week 1-2|Sow|Pot individually 1 cm deep; lupins hate root disturbance later. Plant out at 45 cm while young.
Spring|Rosettes|Water droplets sit like mercury on the leaf hands. Guard young growth from slugs.
Early summer|Spires|Dense pea-flower spikes in improbable colours. Bumblebees work them all day.
After bloom|Deadhead before seed|Cut spikes as they fade — seedlings rarely match the parent — and enjoy the second flush. Plants are short-lived; let one pod self-sow as succession anyway.
""",
            ["Hollyhock"] = """
Day 1 (early summer)|Sow for next year|Sow 1 cm deep where they'll flower, or in pots to move in autumn. Against a wall or fence is traditional and practical — it's their windbreak.
Autumn|Set the rosettes|Plant out 60 cm apart. Big leafy rosettes overwinter.
Next summer|Towers|Spikes climb 2 m or more, studded with silk saucers. Stake in open positions.
All season|Live with rust|The orange-spotted leaves lower down are hollyhock rust — near-universal. Strip the worst leaves; the flowers don't care.
After bloom|Cut and re-seed|Cut stalks down and let a pod or two shed. Hollyhocks are short-lived but immortal by self-sowing along the same wall for generations.
""",
            ["Nasturtium"] = """
Day 1 (after frost)|Poke seeds in poor soil|Push the big wrinkled seeds 2 cm deep where they'll grow. Rich soil = all leaf, no flower. Neglect is the fertiliser here.
Days 7-12|Germination|Fast, chunky seedlings kids can watch. Thin to 30 cm.
Weeks 4-16|Bloom and lunch|Flowers, leaves and buds are all peppery-edible — flowers straight into salads. Trailing types tumble beautifully from pots.
Aphid season|Sacrificial duty|Blackfly cluster on nasturtiums instead of your beans. That's a feature: snip off infested shoots and bin them.
Autumn|Seed for next year|The chickpea-sized seeds drop and return next year on their own, or gather and dry a pocketful.
""",
            ["Morning Glory"] = """
Day 1|Nick, soak, sow warm|Nick the hard seeds, soak overnight, then sow 1 cm deep in individual pots at 20°C. Cold nights stunt them for life, so don't rush the season.
After frost|Plant at the base of strings|Set out by netting, wires or an obelisk in full sun. Poor-to-average soil keeps flowers ahead of foliage.
Weeks 2-6|The spiral climb|Stems corkscrew 3 m upward in weeks. Guide them early; they do the rest.
Summer|One-day flowers|Each trumpet opens at dawn and dies by afternoon, but dozens replace it every morning. Best planted where you'll see it with your coffee.
Autumn|Collect or compost|In warm areas it self-sows; elsewhere collect the seed capsules once brown.
""",
            ["Wisteria"] = """
Day 1|Buy grafted, plant forever|Buy a named, grafted plant (seedlings can take 15 years to flower) and plant against a serious support — masonry wires, a pergola. Think decades.
Year 1|Water and train|Water deeply through the first summer. Tie the main stems along the wires you want them to own.
Every August|Summer prune|Cut all the whippy side shoots back to about six leaves. This is one half of the flowering secret.
Every February|Winter prune|Shorten those same shoots again to two or three buds — fat flower buds, not slim leaf buds, remain. That's the other half.
May|The cascade|Half-metre racemes drip from bare wood, scenting the street. A pruned wisteria flowers harder every single year.
""",
            ["Lilac"] = """
Day 1 (autumn)|Plant in sun and lime|Lilacs love alkaline soil — this is one shrub that appreciates a handful of lime in acid gardens. Full sun, room for a 3 m shrub.
Year 1|Water in|Deep-soak fortnightly through dry spells and mulch. Growth is steady, not fast.
Each May|Two perfumed weeks|The scent defines late spring. Cut generous armfuls — cutting is pruning done right, and it never has more flowers than now.
Right after bloom|Deadhead and prune|Snap off spent heads and do any shaping immediately — next year's buds form by midsummer, so late pruning = no flowers.
Old plants|Renovate by thirds|Ancient leggy lilacs can be cut hard: remove a third of the oldest stems at the base each year for three years.
""",
            ["Gardenia"] = """
Day 1|Pot in acid compost|Ericaceous compost, a bright spot with no midday sun, and warmth — gardenias are demanding and worth it.
Ongoing|Rainwater only|Water with rainwater or cooled boiled water; hard tap water yellows the leaves. Keep evenly moist, never sodden.
Humidity|Fake the subtropics|Stand the pot on a tray of wet pebbles. Dry air is the main reason buds drop before opening.
Bud to bloom|Don't move the pot|Gardenias drop buds if relocated or if night temperatures swing. Pick its spot before buds form, then leave it alone.
Bloom|The reward|One open flower perfumes a room like nothing else in the plant kingdom. Feed monthly with ericaceous fertiliser through summer.
""",
            ["Hibiscus"] = """
Day 1|Warmth and sun|Pot into rich compost and give it your sunniest warm spot — a south window indoors, or outside only once nights stay above 12°C.
Ongoing|Water generously|In summer growth, water when the surface dries — often daily in pots — and feed weekly with high-potash fertiliser.
Bloom|A parade of one-day flowers|Each dinner-plate bloom lasts a day or two, but a happy plant produces them continuously for months.
Bud drop?|Check for stress|Dropped buds mean a move, a draught, or dry roots. Fix the routine and it resumes.
Winter|Rest indoors|Bring pots in before frost to a bright cool room, water sparingly, and prune by a third in spring as growth restarts.
""",
            ["Bird of Paradise"] = """
Day 1|Big pot, big light|Pot into a large container in your brightest spot — a few hours of direct sun indoors is ideal. This is a plant that thinks it's a small tree.
Years 1-3|Grow the fan|Water when the top inch dries, feed monthly in summer, and wipe the paddle leaves. Be warned: it will not flower until mature — often 4-6 years old.
The trick|Keep it pot-bound|Crowded roots trigger flowering. Resist repotting once it fills a big pot; top-dress instead.
Flowering|The bird emerges|The beak-like bract tilts horizontal and orange-and-blue crests unfold in sequence — one of the most theatrical flowers on Earth.
Ongoing|Patience pays|Mature plants flower yearly, mostly in the cooler months. Split leaves are natural, not damage.
""",
            ["Lotus"] = """
Day 1 (spring)|A tub of mud|Half-fill a large watertight tub (no drainage holes) with heavy loam — not compost, which floats. Lay the banana-like tuber horizontally, barely covered, growing tip up and free.
Week 1-4|Add water slowly|Start with 5 cm of water above the soil. As leaves rise, raise the level to 15-30 cm. Full sun and warmth are non-negotiable.
Summer|Aerial leaves|First come floating leaves, then the great parasols that shed water in silver beads.
High summer|The bloom|Flowers open for three days running, closing each night, then the shower-head seed pod remains. Sacred to half the world for good reason.
Winter|Below the ice line|Sink the tub deeper or move it somewhere the tuber won't freeze solid. It reawakens in late spring without fail.
""",
            ["Forget-me-not"] = """
Day 1 (early summer)|Sow for next spring|Scatter seed where it'll flower — part shade, moist soil — and rake in lightly. It's a biennial: leaves this year, the blue haze next spring.
Autumn|Rosettes everywhere|Thin or transplant the fuzzy rosettes 15 cm apart, ideally around tulips and under shrubs.
Spring|The blue haze|Clouds of tiny sky-blue flowers with golden eyes, exactly when tulips need a skirt.
Weeks of bloom|No care at all|Genuinely zero maintenance while flowering.
After bloom|Pull and shake|When plants turn grey and tired, pull them up and shake them where you want next year's haze — they self-sow with total reliability.
""",
            ["Primrose"] = """
Day 1 (autumn or early spring)|Plant in dappled shade|Set plants 20 cm apart in moist, humus-rich soil — under deciduous shrubs, along a shady path, the foot of a hedge.
Late winter|First flowers|Pale yellow blooms open with the snowdrops, weeks before spring proper.
Spring|Peak|Clumps flower for two months. Pick a tiny posy — the scent is honey-faint but real.
After flowering|Keep moist|Their one demand: don't let summer bake them dry. Mulch with leaf mould.
Every 3 years|Divide|Lift and split clumps right after flowering, replant the divisions, and the colony spreads down the path year by year.
""",
            ["Black-eyed Susan"] = """
Day 1 (spring)|Sow or plant|Sow 3 mm deep indoors six weeks before frost ends, or plant nursery starts 40 cm apart in full sun. Any decent soil works.
Weeks 2-8|Tough youth|Water while establishing; after that this prairie native handles drought better than almost any border plant.
Midsummer|Gold rush|Masses of golden daisies with chocolate centres, weeks on end, swarming with butterflies.
Autumn|Leave the seed heads|Deadhead early on for more flowers, but leave the last flush standing: goldfinches ride the stems all winter picking seeds.
Every 3-4 years|Divide in spring|Split expanding clumps to keep vigour up and to colonise the rest of the sunny border.
""",
            ["Coneflower"] = """
Day 1 (spring)|Plant, don't pamper|Set plants 45 cm apart in full sun and ordinary well-drained soil. Skip the fertiliser — soft growth flops.
Year 1|Water to establish|Weekly soaks in the first summer buy you a decade of drought-proof performance.
Summer|Bloom|Rosy-purple shuttlecocks around a burnt-orange cone, each lasting weeks. Butterflies queue for landing rights.
Autumn-winter|Stand tall|Leave stems and cones standing: frost rims them beautifully and goldfinches strip the seeds.
Spring|Cut back and divide|Cut old stems down as new basal growth shows. Divide only every 4-5 years — established clumps resent disturbance.
""",
            ["Bleeding Heart"] = """
Day 1 (early spring)|Plant in shade|Set the brittle roots carefully in rich, moist soil in dappled shade, crown 3 cm deep, 60 cm from neighbours it will briefly tower over.
Weeks 2-6|The rise|Ferny shoots unfurl fast in cool spring weather. Keep soil moist; that's the whole job.
Mid-late spring|Lockets on arches|Rose-pink hearts dangle in perfect rows from arching stems — one of the garden's most romantic sights. Superb picked for the vase.
Early summer|The vanishing act|Foliage yellows and the whole plant dies to the ground by July. It's not dead — it's asleep. Mark the spot so you don't dig into it.
Every spring|Reliable return|It reappears fatter each year. Divide huge clumps in early spring if you want to spread the romance.
"""
        };
    }
}
