# H4_PriceTracker

## Data-flow

### Definitions

- Function: `(f)`
- File/Database: `|x|`
- Input/Output: `[x]`
- Flow: `->`

`[Sitemap XML] -> (Extract Product URLs) -> |Kafka Cluster| -> (Scrape Product) -> |Couch DB|`
