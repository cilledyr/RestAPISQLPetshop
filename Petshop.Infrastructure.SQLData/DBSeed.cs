using Petshop.Core.DomainService;
using Petshop.Core.Enteties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Petshop.Infrastructure.Data
{
    public static class DBSeed
    {


        public static void InitData(PetshopAppContext ctx)
        {
            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();

            List<PetColor> allTheColors = new List<PetColor>();

            List<PetColor> allThePetColors = new List<PetColor>
            {
                new PetColor {PetColorName = "Black"},
                new PetColor {PetColorName = "White"},
                new PetColor {PetColorName = "Brown"},
                new PetColor {PetColorName = "Gray"},
                new PetColor {PetColorName = "Purple"},
                new PetColor {PetColorName = "Blue"},
                new PetColor {PetColorName = "Green"},
                new PetColor {PetColorName = "Orange"}
            };

            foreach(var color in allThePetColors)
            {
                var theColor = ctx.Add(color).Entity;
                allTheColors.Add(theColor);
            }

            List<PetType> allTheTypes = new List<PetType>();

            List<PetType> allPetTypes = new List<PetType>
            {
                new PetType {PetTypeName = "Cat"},
                new PetType {PetTypeName = "Dog"},
                new PetType {PetTypeName = "Horse"},
                new PetType {PetTypeName = "Fish"},
                new PetType {PetTypeName = "Gerbil"},
                new PetType {PetTypeName = "Hamster"},
                new PetType {PetTypeName = "Rabbit"}
            };

            foreach (var petType in allPetTypes)
            {
                var theType = ctx.Add(petType).Entity;
                allTheTypes.Add(theType);
            }
            //ctx.SaveChanges();


            List<Owner> theOwners = new List<Owner>();

            List<Owner> allOwners = new List<Owner>
            {
                new Owner{OwnerFirstName = "Lars", OwnerLastName = "Rasmussen", OwnerAddress = "SweetStreet 4, 6700 Esbjerg", OwnerPhoneNr = "+45 1234 5678", OwnerEmail = "lars@rasmussen.dk"},
                new Owner{OwnerFirstName = "John", OwnerLastName = "Jackson", OwnerAddress = "The Alley 6, 6705 Esbjerg Ø", OwnerPhoneNr = "+45 2549 6254", OwnerEmail = "thesuper_awesome@hotmail.com"},
                new Owner{OwnerFirstName = "Maria", OwnerLastName = "Saunderson", OwnerAddress = "Kongensgade 33, 6700 Esbjerg", OwnerPhoneNr = "+45 8761 1624", OwnerEmail = "suuper_sexy88@gmail.com"},
                new Owner{OwnerFirstName = "Belinda", OwnerLastName = "Twain", OwnerAddress = "Nørregade 14, 6700 Esbjerg", OwnerPhoneNr = "+45 7365 5976", OwnerEmail = "blender_wizard@hotmail.com"},
                new Owner{OwnerFirstName = "Roald", OwnerLastName = "Schwartz", OwnerAddress = "Lark Road 26, 6715 Esbjerg N", OwnerPhoneNr = "+45 7618 5234", OwnerEmail = "the_cool_roald@msnmail.com"},
                new Owner{OwnerFirstName = "Shiela", OwnerLastName = "Jesperson", OwnerAddress = "Daniels Road 45, 6700 Esbjerg", OwnerPhoneNr = "+45 7831 2561", OwnerEmail = "shiela45@gmail.com"},
                new Owner{OwnerFirstName = "Hansi", OwnerLastName = "Thompson", OwnerAddress = "Spooky Road 666, 6705 Esbjerg Ø", OwnerPhoneNr = "+45 1465 2845", OwnerEmail = "theghost83@outlook.com"},
                new Owner{OwnerFirstName = "Victoria", OwnerLastName = "Marks", OwnerAddress = "Birkelunden 8, 6705 Esbjerg Ø", OwnerPhoneNr = "+45 5956 4651", OwnerEmail = "vicmarks@hotmail.com"},
                new Owner{OwnerFirstName = "Niels", OwnerLastName = "Billson", OwnerAddress = "Folevej 3, 6715 Esbjerg N", OwnerPhoneNr = "+45 7286 9435", OwnerEmail = "ne49billson@gmail.com"},
                new Owner{OwnerFirstName = "Emanuelle", OwnerLastName = "Johnson", OwnerAddress = "Foldgårdsvej 17, 6715 Esbjerg N", OwnerPhoneNr = "+45 7315 4255", OwnerEmail = "emanuelle-actor@outlook.com"}
            };
            foreach (var owner in allOwners)
            {
                var theOwner = ctx.Add(owner).Entity;
                theOwners.Add(theOwner);
            }
            //ctx.SaveChanges();

            List<Pet> allPets = new List<Pet> {
                new Pet {PetBirthday = DateTime.Now.AddDays(-25), PetColor = new List<PetColorPet> { new PetColorPet { petColor = allTheColors[3] } }, PetName = "Hans", PetPreviousOwner = "Aniyah Chan", PetOwner= theOwners[0], PetSoldDate = DateTime.Now.AddMonths(0), PetType = allTheTypes[0], PetPrice = 10 },
                new Pet {PetBirthday = DateTime.Now.AddDays(-400), PetColor = new List<PetColorPet> { new PetColorPet { petColor = allTheColors[0] }, new PetColorPet { petColor = allTheColors[1] } }, PetName = "Katia", PetPreviousOwner = "Alison Melia", PetOwner= theOwners[1], PetSoldDate = DateTime.Now.AddMonths(-3), PetType = allTheTypes[1], PetPrice = 235 },
                new Pet {PetBirthday = DateTime.Now.AddDays(-320), PetColor = new List<PetColorPet> { new PetColorPet { petColor = allTheColors[2] }}, PetName = "Jellybelly", PetPreviousOwner = "Abdallah Dejesus", PetOwner= theOwners[2], PetSoldDate = DateTime.Now.AddMonths(-5), PetType = allTheTypes[2], PetPrice = 2 },
                new Pet {PetBirthday = DateTime.Now.AddDays(-50), PetColor = new List<PetColorPet> { new PetColorPet { petColor = allTheColors[0] }}, PetName = "Faithful", PetPreviousOwner = "Teegan Boyer", PetOwner= theOwners[3], PetSoldDate = DateTime.Now.AddMonths(-1), PetType = allTheTypes[1], PetPrice = 41 },
                new Pet {PetBirthday = DateTime.Now.AddDays(-81), PetColor = new List<PetColorPet> { new PetColorPet { petColor = allTheColors[0] }, new PetColorPet { petColor = allTheColors[7] }}, PetName = "Enigma", PetPreviousOwner = "Vinnie Odling", PetOwner= theOwners[4], PetSoldDate = DateTime.Now.AddMonths(-2), PetType = allTheTypes[3], PetPrice = 56 },
                new Pet {PetBirthday = DateTime.Now.AddDays(-691), PetColor = new List<PetColorPet> { new PetColorPet { petColor = allTheColors[4] } }, PetName = "Bob", PetPreviousOwner = "Amina Brookes", PetOwner= theOwners[4], PetSoldDate = DateTime.Now.AddMonths(-8), PetType = allTheTypes[3], PetPrice = 98 },
                new Pet {PetBirthday = DateTime.Now.AddDays(-259), PetColor = new List<PetColorPet> { new PetColorPet { petColor = allTheColors[0] }, new PetColorPet { petColor = allTheColors[3] } }, PetName = "Linea", PetPreviousOwner = "Carmel Livingson", PetOwner= theOwners[5], PetSoldDate = DateTime.Now.AddMonths(-3), PetType = allTheTypes[4], PetPrice = 59 },
                new Pet {PetBirthday = DateTime.Now.AddDays(-856), PetColor = new List<PetColorPet> { new PetColorPet { petColor = allTheColors[2] } }, PetName = "Tommy", PetPreviousOwner = "Nicole Jaramillo", PetOwner= theOwners[6], PetSoldDate = DateTime.Now.AddMonths(-15), PetType = allTheTypes[5], PetPrice = 76 },
                new Pet {PetBirthday = DateTime.Now.AddDays(-1576), PetColor = new List<PetColorPet> { new PetColorPet { petColor = allTheColors[0] } }, PetName = "Beauty", PetPreviousOwner = "Hibah Bartlet", PetOwner= theOwners[7], PetSoldDate = DateTime.Now.AddMonths(-21), PetType = allTheTypes[6], PetPrice = 1090 },
                new Pet {PetBirthday = DateTime.Now.AddDays(-10), PetColor = new List<PetColorPet> { new PetColorPet { petColor = allTheColors[1] } }, PetName = "Beatrice", PetPreviousOwner = "Radhika Baird", PetOwner= theOwners[8], PetSoldDate = DateTime.Now, PetType = allTheTypes[3], PetPrice = 28 },
                new Pet {PetBirthday = DateTime.Now.AddDays(-33), PetColor = new List<PetColorPet> { new PetColorPet { petColor = allTheColors[1]  }, new PetColorPet { petColor = allTheColors[7] } }, PetName = "Jumpy", PetPreviousOwner = "Havin Boyle", PetOwner= theOwners[0], PetSoldDate = DateTime.Now.AddMonths(-1), PetType = allTheTypes[2], PetPrice = 100 },
                new Pet {PetBirthday = DateTime.Now.AddDays(-63), PetColor = new List<PetColorPet> { new PetColorPet { petColor = allTheColors[2]  }, new PetColorPet { petColor = allTheColors[4] } }, PetName = "Cujo", PetPreviousOwner = "Franklin Barajas", PetOwner= theOwners[9], PetSoldDate = DateTime.Now.AddMonths(-1), PetType = allTheTypes[6], PetPrice = 346 },
                new Pet {PetBirthday = DateTime.Now.AddDays(-18), PetColor = new List<PetColorPet> { new PetColorPet { petColor = allTheColors[5] } }, PetName = "Shenna", PetPreviousOwner = "Jovan Bloggs", PetOwner= theOwners[1], PetSoldDate = DateTime.Now, PetType = allTheTypes[3], PetPrice = 865 },
                new Pet {PetBirthday = DateTime.Now.AddDays(-156), PetColor = new List<PetColorPet> { new PetColorPet { petColor = allTheColors[6] } }, PetName = "Firehoof", PetPreviousOwner = "Aamna Atherton", PetOwner= theOwners[7], PetSoldDate = DateTime.Now.AddMonths(-3), PetType = allTheTypes[0], PetPrice = 2096 }

            };
            
            foreach (var pet in allPets)
            {
                ctx.Add(pet);
            }

            List<User> allUsers = new List<User>
            {
                new User {UserName = "AdminAnn", UserPassword = "Admin1234", UserIsAdmin = true},
                new User {UserName = "UserUlrik", UserPassword ="User1234", UserIsAdmin = false}
            };

            foreach (var user in allUsers)
            {
                ctx.Add(user);
            }

            ctx.SaveChanges();
        }
    }
}
