using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Resend;

namespace SmokeTest.Pages;

public class IndexModel : PageModel
{
    private static readonly string[] ProjectTypes =
    [
        "Business website", "Web app or portal", "E-commerce experience", "Something else"
    ];

    private readonly IResend _resend;
    private readonly IConfiguration _configuration;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(IResend resend, IConfiguration configuration, ILogger<IndexModel> logger)
    {
        _resend = resend;
        _configuration = configuration;
        _logger = logger;
    }

    [BindProperty, Required, StringLength(80)]
    public string FirstName { get; set; } = string.Empty;

    [BindProperty, Required, StringLength(80)]
    public string LastName { get; set; } = string.Empty;

    [BindProperty, Required, EmailAddress, StringLength(254)]
    public string Email { get; set; } = string.Empty;

    [BindProperty, Required]
    public string Project { get; set; } = string.Empty;

    [BindProperty, Required, StringLength(4000, MinimumLength = 10)]
    public string Message { get; set; } = string.Empty;

    public string? ContactStatus { get; private set; }
    public bool ContactSent { get; private set; }

    public async Task<IActionResult> OnPostSendAsync(CancellationToken cancellationToken)
    {
        FirstName = FirstName.Trim();
        LastName = LastName.Trim();
        Email = Email.Trim();
        Message = Message.Trim();

        if (string.IsNullOrWhiteSpace(FirstName))
        {
            ModelState.AddModelError(nameof(FirstName), "Enter your first name.");
        }

        if (string.IsNullOrWhiteSpace(LastName))
        {
            ModelState.AddModelError(nameof(LastName), "Enter your last name.");
        }

        if (string.IsNullOrWhiteSpace(Message))
        {
            ModelState.AddModelError(nameof(Message), "Enter a message.");
        }

        if (!ProjectTypes.Contains(Project, StringComparer.Ordinal))
        {
            ModelState.AddModelError(nameof(Project), "Choose a project type from the list.");
        }

        if (!ModelState.IsValid)
        {
            ContactStatus = "Please check the form and correct the highlighted fields.";
            return Page();
        }

        var apiKey = _configuration["RESEND_API_KEY"];
        var fromAddress = _configuration["RESEND_FROM_EMAIL"];
        if (string.IsNullOrWhiteSpace(apiKey) || string.IsNullOrWhiteSpace(fromAddress))
        {
            _logger.LogError("Contact form email is unavailable because Resend is not configured.");
            ContactStatus = "The contact form is temporarily unavailable. Please email langasonwabile1993@gmail.com directly.";
            return Page();
        }

        var fullName = $"{FirstName.Trim()} {LastName.Trim()}";
        var message = new EmailMessage
        {
            From = fromAddress,
            Subject = "New portfolio contact form message",
            ReplyTo = Email.Trim(),
            TextBody = $"Name: {fullName}\nEmail: {Email.Trim()}\nProject: {Project}\n\n{Message.Trim()}"
        };
        message.To.Add("langasonwabile1993@gmail.com");

        try
        {
            await _resend.EmailSendAsync(message, cancellationToken);
            ContactSent = true;
            ContactStatus = "Thanks for reaching out. Your message has been sent.";
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            Project = string.Empty;
            Message = string.Empty;
            ModelState.Clear();
        }
        catch (ResendException exception)
        {
            _logger.LogError(exception, "Resend rejected a portfolio contact form message.");
            ContactStatus = "We couldn’t send your message right now. Please try again or email langasonwabile1993@gmail.com directly.";
        }
        catch (HttpRequestException exception)
        {
            _logger.LogError(exception, "Could not connect to Resend for a portfolio contact form message.");
            ContactStatus = "We couldn’t send your message right now. Please try again or email langasonwabile1993@gmail.com directly.";
        }

        return Page();
    }

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
