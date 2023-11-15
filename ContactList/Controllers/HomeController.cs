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

    [HttpGet]
    public IActionResult Edit(Guid id)
    {
        var contactToEdit = _context.Contacts.FirstOrDefault(contact => contact.Id == id);
        var model = new EditModalModel();

        if (contactToEdit != null)
        {
            model.Id = contactToEdit.Id;
            model.Name = contactToEdit.Name;
            model.BirthDate = contactToEdit.BirthDate.ToString();
            model.JobTitle = contactToEdit.JobTitle;
            model.MobilePhone = contactToEdit.MobilePhone;
        }

        return PartialView("_ContactEditModalPartial", model);
    }

    [HttpPost]
    public IActionResult Edit(EditModalModel updatedModel)
    {
        Contact contactToUpdate = new Contact(updatedModel.Id, updatedModel.Name, updatedModel.MobilePhone,
            updatedModel.JobTitle,
            DateOnly.Parse(updatedModel.BirthDate!));
        _context.Contacts.Update(contactToUpdate);
        _context.SaveChanges();

        return PartialView("_ContactEditModalPartial", updatedModel);
    }
// TODO: PARSE BUTTON NAME IN EDIT, AND PERFORM EDIT AS DELETE;
    [HttpPost]
    public IActionResult Delete(EditModalModel updatedModel)
    {
        var contactToDelete = _context.Contacts.FirstOrDefault(contact => contact.Id == updatedModel.Id);
        _context.Contacts.Remove(contactToDelete!);
        _context.SaveChanges();
    
        return PartialView("_ContactEditModalPartial", updatedModel);
    }
}