using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmokeTest.Pages;

public class IndexModel : PageModel
{
    public IReadOnlyList<string> ProjectCategories { get; } =
    [
        "E-commerce", "Marketing", "Beauty", "Directory", "Trades", "Hospitality",
        "Operations", "Education", "Wellness", "Business", "Commerce", "Corporate"
    ];

    public IReadOnlyList<PortfolioProject> Projects { get; } =
    [
        new("01", "E-commerce", "Codia Design Spark", "Fashion e-commerce storefront", "project-art--fashion", "CODIA", "Wear your point of view.", "CD"),
        new("02", "Marketing", "Digital Dove Nest", "Marketing analytics dashboard", "project-art--marketing", "DOVE NEST", "Make every move count.", "↗"),
        new("03", "Beauty", "Lash Allure Beauty Boutique", "Lash studio, courses & product shop", "project-art--beauty", "LASH ALLURE", "Your moment. Your look.", "LA"),
        new("04", "Directory", "Heiderand Centre Guide", "Local business directory — Mossel Bay", "project-art--directory", "HEIDERAND", "Find your neighbourhood.", "⌖"),
        new("05", "Trades", "Garden Route Plumbing Pro", "Plumbing & renovation services — Mossel Bay", "project-art--trades", "GR PLUMBING", "We keep things flowing.", "GR"),
        new("06", "Hospitality", "Village Square Deli", "Charcuterie, platters & reservations — Stilbaai", "project-art--deli", "VILLAGE SQUARE", "Gather around the good stuff.", "VS"),
        new("07", "Operations", "Skydive Mossel Bay", "Manifest & weather ops for tandem skydiving", "project-art--sky", "SKYDIVE MOSSEL BAY", "A view worth the leap.", "↓"),
        new("08", "Education", "Development CLC", "Community learning & development platform", "project-art--learning", "DEVELOPMENT CLC", "Room to learn. Space to grow.", "CLC"),
        new("09", "Education", "South Cape College", "Student assistant for TVET campus life", "project-art--college", "SOUTH CAPE", "Your next chapter starts here.", "SC"),
        new("10", "Wellness", "Wild Bhodi", "Online yoga & wellness studio", "project-art--wellness", "WILD BHODI", "Find your own kind of balance.", "WB"),
        new("11", "Business", "Marclink Technologies", "Tech repair & IT solutions — Mossel Bay", "project-art--tech", "MARCLINK", "Good tech. No guesswork.", "M"),
        new("12", "E-commerce", "TechFix Pro", "Device repair & phone parts storefront", "project-art--repair", "TECHFIX PRO", "Fix it. Find the right part.", "TF"),
        new("13", "Commerce", "Bag Swatches", "Product swatch showcase experience", "project-art--swatches", "BAG SWATCHES", "Pick your colour story.", "BS"),
        new("14", "Corporate", "Imfundo Digital Hub", "Digital innovation hub presence", "project-art--corporate", "IMFUNDO", "Ideas into the digital future.", "ID"),
        new("15", "Hospitality", "Downtown Accommodation", "Hospitality booking & guest stays", "project-art--stay", "DOWNTOWN STAYS", "Make yourself at home.", "DS")
    ];
}

public sealed record PortfolioProject(
    string Number,
    string Category,
    string Name,
    string Description,
    string ArtClass,
    string ArtTitle,
    string ArtHeading,
    string ArtMark);
