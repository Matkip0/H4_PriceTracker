using parser;
using parser.Models;

Scraper scraper = new Scraper();

List<string> sites =
    new List<string>
    {
        "https://www.fcomputer.dk/nintendo-switch-lite-haandholdt-spillekontrolenhed-blaa",
        "https://www.fcomputer.dk/gamer/udstyr/aoc-gaming-c27g4zxu-skaerm-c27g4zxu",
        "https://www.fcomputer.dk/gear4u-monitor-arm-for-desk-single-gasfjerderloeft-g4u-206001"
    };

List<Product> products = scraper.GetProducts(sites);
Console.WriteLine(products);
// Product product =
    // scraper.GetProductDetails(@"https://www.fcomputer.dk/nintendo-switch-lite-haandholdt-spillekontrolenhed-blaa");