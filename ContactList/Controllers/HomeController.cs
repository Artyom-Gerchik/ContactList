using System.Diagnostics;
using ContactList.Data;
using ContactList.Entities;
using Microsoft.AspNetCore.Mvc;
using ContactList.Models;

namespace ContactList.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;


    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(new HomePageModel
        {
            Contacts = _context.Contacts.ToList()
        });
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpGet]
    public IActionResult Create()
    {
        AddModalModel newContact = new AddModalModel();
        return PartialView("_ContactAddModalPartial", newContact);
    }

    [HttpPost]
    public IActionResult Create(AddModalModel newContact)
    {
        Contact contactToAdd = new Contact(Guid.NewGuid(), newContact.Name, newContact.MobilePhone, newContact.JobTitle,
            DateOnly.Parse(newContact.BirthDate!));

        _context.Contacts.Add(contactToAdd);
        _context.SaveChanges();

        return PartialView("_ContactAddModalPartial", newContact);
    }
}