using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Garage_2._0.DataAccessLayer;

namespace Garage_2._0.Models
{
    public class ParkedVehicles2Controller : Controller
    {
        private RegisterContext db = new RegisterContext();
        private const int NumberOfParkingSlots = 20;

        // GET: ParkedVehicles2
        public ActionResult Index(string FilterString = null)
        {

            var VehicleSearch = db.ParkedVehicles
                .Where(e => FilterString == null || e.VehicleTypeName.Contains(FilterString)
                            || e.RegNr.Contains(FilterString) || e.Member.FirstName.Contains(FilterString)
                            || e.TimeStamp.ToString().Contains(FilterString) || e.Member.LastName.Contains(FilterString))
                .Select(e => new ParkedVehiclesViewModel()
                {
                    Id = e.Id,
                    Owner = e.Member.FirstName + " " + e.Member.LastName,
                    Type = e.VehicleTypeName,
                    RegNr = e.RegNr,
                    TimeStamp = e.TimeStamp
                }).ToList();
        
            return View(VehicleSearch);


            //var parkedVehicles = db.ParkedVehicles.Include(p => p.Member).Include(p => p.VehicleType);
            //return View(parkedVehicles.ToList());
        }

        // GET: ParkedVehicles2
        public ActionResult DetailedView(string FilterString = null)
        {
            var VehicleSearch = db.ParkedVehicles
                  .Where(e => FilterString == null || e.VehicleTypeName.Contains(FilterString)
                              || e.Model.Contains(FilterString) || e.Make.Contains(FilterString)
                              || e.RegNr.Contains(FilterString) || e.Member.FirstName.Contains(FilterString)
                               || e.Color.Contains(FilterString) || e.TimeStamp.ToString().Contains(FilterString))
                  .Select(e => new ParkedVehiclesViewModel()
                  {
                      Owner = e.Member.FirstName,
                      Type = e.VehicleTypeName,
                      RegNr = e.RegNr,
                      Color = e.Color,
                      Make = e.Make,
                      Model = e.Model,
                      NrOfWheels = e.NrOfWheels,
                      TimeStamp = e.TimeStamp,
                      ParkingSlot = e.ParkingSlot
                  }).ToList();

            return View(VehicleSearch);
            // || e.TimeStamp.ToString().Contains(FilterString)
            //var parkedVehicles = db.ParkedVehicles.Include(p => p.Member).Include(p => p.VehicleType);
            //return View(parkedVehicles.ToList());
        }

        // GET: ParkedVehicles2/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkedVehicle parkedVehicle = db.ParkedVehicles.Find(id);
            if (parkedVehicle == null)
            {
                return HttpNotFound();
            }
            return View(parkedVehicle);
        }

        // GET: ParkedVehicles2/Create
        public ActionResult Park()
        {
            ViewBag.MemberId = new SelectList(db.Members, "Id", "FullName");
            ViewBag.VehicleTypeName = new SelectList(GetParkableTypes(), "Name", "Name");
            return View();
        }

        public ActionResult Search(string FilterString = null)
        {

            var VehicleSearch = db.ParkedVehicles
                .Where(e => FilterString == null || e.VehicleTypeName.Contains(FilterString)
                            || e.Model.Contains(FilterString) || e.Make.Contains(FilterString)
                            || e.RegNr.Contains(FilterString) || e.Member.FullName.Contains(FilterString)
                            || e.Color.Contains(FilterString))
                .Select(e => new ParkedVehiclesViewModel()
                {
                     Owner = e.Member.FullName,
                     Type = e.VehicleTypeName,
                     RegNr = e.RegNr,
                     Color = e.Color,
                     Make = e.Make,
                     Model = e.Model,
                     NrOfWheels = e.NrOfWheels,
                     TimeStamp = e.TimeStamp,
                     ParkingSlot = e.ParkingSlot
                }).ToList();
            return View(VehicleSearch);
        }

        // POST: ParkedVehicles2/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Park([Bind(Include = "RegNr,Color,Make,Model,NrOfWheels,MemberId,VehicleTypeName")] ParkedVehicle parkedVehicle)
        {
            if (ModelState.IsValid)
            {
                parkedVehicle.TimeStamp = DateTime.Now;
                var Slot = GetNextFreeParkingSlot(db.VehicleTypes.Where(x => x.Name == parkedVehicle.VehicleTypeName).Select(x => x.Size).FirstOrDefault());
                if (Slot >= 0)
                {
                    parkedVehicle.ParkingSlot = Slot;
                    db.ParkedVehicles.Add(parkedVehicle);
                    db.SaveChanges();
                    return RedirectToAction("Details", "ParkedVehicles2", new { Id = parkedVehicle.Id });
                }
                else
                {
                    Response.Write("<script type=\"text/javascript\">alert('Failed attempt to park a vehicle due to size constraints');</script>");
                }
                return RedirectToAction("Index");
            }

            ViewBag.MemberId = new SelectList(db.Members, "Id", "FirstName", parkedVehicle.MemberId);
            ViewBag.VehicleTypeName = new SelectList(db.VehicleTypes, "Name", "Name", parkedVehicle.VehicleTypeName);
            return View(parkedVehicle);
        }

        // GET: ParkedVehicles2/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkedVehicle parkedVehicle = db.ParkedVehicles.Find(id);
            if (parkedVehicle == null)
            {
                return HttpNotFound();
            }
            ViewBag.MemberId = new SelectList(db.Members, "Id", "FirstName", parkedVehicle.MemberId);
            ViewBag.VehicleTypeName = new SelectList(db.VehicleTypes, "Name", "Name", parkedVehicle.VehicleTypeName);
            return View(parkedVehicle);
        }

        // POST: ParkedVehicles2/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RegNr,Color,Make,Model,NrOfWheels,TimeStamp,ParkingSlot,MemberId,VehicleTypeName")] ParkedVehicle parkedVehicle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(parkedVehicle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MemberId = new SelectList(db.Members, "Id", "FirstName", parkedVehicle.MemberId);
            ViewBag.VehicleTypeName = new SelectList(db.VehicleTypes, "Name", "Name", parkedVehicle.VehicleTypeName);
            return View(parkedVehicle);
        }

        // GET: ParkedVehicles2/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkedVehicle parkedVehicle = db.ParkedVehicles.Find(id);
            if (parkedVehicle == null)
            {
                return HttpNotFound();
            }
            return View(parkedVehicle);
        }

        // POST: ParkedVehicles2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ParkedVehicle parkedVehicle = db.ParkedVehicles.Find(id);
            RecieptViewModel CheckoutVehicle = new RecieptViewModel
            {
                Id = parkedVehicle.Id,
                Owner = parkedVehicle.Member.FullName,
                RegNr = parkedVehicle.RegNr,
                TimeStamp = parkedVehicle.TimeStamp,
                Type = parkedVehicle.VehicleTypeName
            };

            db.ParkedVehicles.Remove(parkedVehicle);
            db.SaveChanges();
 
            return RedirectToAction("Reciept", CheckoutVehicle);
        }

        public ActionResult Reciept(RecieptViewModel checkedoutVehicle)
        {
            if (ModelState.IsValid)
            {
                return View(checkedoutVehicle);
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private int[] GetCurrentParkingState()
        {
            int[] Slots = new int[NumberOfParkingSlots];
            var parked = db.ParkedVehicles;

            foreach (var vehicle in parked)
            {
                if (vehicle.VehicleType.Size == 1)
                {
                    Slots[vehicle.ParkingSlot] += vehicle.VehicleType.Size;
                }
                else
                {
                    for (int i = 0; i < vehicle.VehicleType.Size / 3; i++)
                    {
                        Slots[vehicle.ParkingSlot + i] += 3;
                    }
                }
            }
            return Slots;
        }
        private List<VehicleType> GetParkableTypes()
        {
            var CurrentState = GetCurrentParkingState();
            int PotentialParkingSpace = 0;
            for (int i = 0; i < NumberOfParkingSlots; i++)
            {
                if (CurrentState[i] == 3) { continue; }
                if (CurrentState[i] > 0 && CurrentState[i] < 3)
                {
                    if (PotentialParkingSpace < 3) { PotentialParkingSpace = 1; continue; }
                }
                if (CurrentState[i] == 0)
                {
                    int thisfreeslot = 0;
                    for (int x = i; x < NumberOfParkingSlots; x++)
                    {
                        if (CurrentState[x] != 0) { x = NumberOfParkingSlots; }
                        else { thisfreeslot += 3; }
                    }
                    if (thisfreeslot > PotentialParkingSpace) { PotentialParkingSpace = thisfreeslot; }
                }
            }
            
            return db.VehicleTypes.Where(x => x.Size <= PotentialParkingSpace).ToList();
        }

        private int GetNextFreeParkingSlot(int VehicleSize)
        {
            var CurrentState = GetCurrentParkingState();
            for (int i = 0; i < NumberOfParkingSlots; i++)
            {
                if (VehicleSize == 1)
                {
                    // First find if there is a parkingslot with one or two motorbikes
                    for (int J = 0; J <CurrentState.Length; J++)
                    {
                        if (CurrentState[J] == 1 || CurrentState[J]==2)
                        {
                            return J;
                        }
                    }
                    //int? SlotWithBikes = CurrentState.FirstOrDefault(x => x==1);
                    //if (!SlotWithBikes.HasValue) { SlotWithBikes = CurrentState.FirstOrDefault(x => x == 2); }
                    //if (SlotWithBikes.HasValue) { return SlotWithBikes.Value; }
                }
                if (CurrentState[i] == 0)
                {
                    var SizeCounter = VehicleSize - 3;
                    for (int x = i + 1; SizeCounter <= 0 && x <= NumberOfParkingSlots; x++)
                    {
                        if ( x < NumberOfParkingSlots )
                        {
                            if (CurrentState[x] == 0) { SizeCounter -= 3; }
                            else { break ; }
                        }
                    }
                    if (SizeCounter == 0) { return i; }
                }
            }
            return -1;
        }
    }
}
