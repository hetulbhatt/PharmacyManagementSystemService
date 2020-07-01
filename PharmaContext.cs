namespace WCFPharma
{
	using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
	using System.Linq;

	public class PharmaContext : DbContext
	{
		// Your context has been configured to use a 'PharmaContext' connection string from your application's 
		// configuration file (App.config or Web.config). By default, this connection string targets the 
		// 'WCFPharma.PharmaContext' database on your LocalDb instance. 
		// 
		// If you wish to target a different database and/or database provider, modify the 'PharmaContext' 
		// connection string in the application configuration file.
		public PharmaContext()
			: base("name=PharmaContext")
		{
		}

		// Add a DbSet for each entity type that you want to include in your model. For more information 
		// on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

		public virtual DbSet<Pharmacist> Pharmacists { get; set; }
		public virtual DbSet<Medicine> Medicines { get; set; }
		public virtual DbSet<Dealer> Dealers { get; set; }
		public virtual DbSet<Manufacturer> Manufacturers { get; set; }
		public virtual DbSet<Ledger> Ledgers { get; set; }
	}

	public class Pharmacist
	{
		[Key]
		public int PharmacistID { get; set; }
		public string Password { get; set; }
		//public Pharmacist(string Password)
		//{
		//	this.Password = Password;
		//	int max;
		//	using (var ctx = new PharmaContext())
		//	{
		//		if(ctx.Pharmacists.Count() == 0)
		//		{
		//			max = 0;
		//		}
		//		else
		//		{
		//			max = (ctx.Pharmacists.Max(p => p.PharmacistID));
		//		}
		//	}
		//	this.PharmacistID = max + 1;
		//}

		//public Pharmacist()
		//{

		//}
	}

	public class Medicine
	{
		public int MedicineID { get; set; }
		public string Name { get; set; }
		public double Price { get; set; }
		public int Stock { get; set; }
		public Dealer Dealer { get; set; }
		public Manufacturer Manufacturer { get; set; }
		public Medicine(string name, double price, int stock, int dealerID, int manufacturerID)
		{
			this.Name = name;
			this.Price = price;
			this.Stock = stock;
			int max;
			using (var ctx = new PharmaContext())
			{
				this.Dealer = ctx.Dealers.Where(d=>d.DealerID==dealerID).FirstOrDefault();
				this.Manufacturer = ctx.Manufacturers.Where(m => m.ManufacturerID == manufacturerID).FirstOrDefault();
				if (ctx.Medicines.Count() == 0)
				{
					max = 0;
				}
				else
				{
					max = (ctx.Medicines.Max(p => p.MedicineID));
				}
				this.MedicineID = max + 1;
				ctx.Medicines.Add(this);
				ctx.SaveChanges();
			}
			
		}

		public Medicine()
		{

		}
	}

	public class Dealer
	{
		public int DealerID { get; set; }
		public string Name { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public string Address { get; set; }

		public Dealer(string name, string phone, string email, string address)
		{
			this.Name = name;
			this.Phone = phone;
			this.Email = email;
			this.Address = address;
			int max;
			using (var ctx = new PharmaContext())
			{
				if (ctx.Dealers.Count() == 0)
				{
					max = 0;
				}
				else
				{
					max = (ctx.Dealers.Max(p => p.DealerID));
				}
				this.DealerID = max + 1;
				ctx.Dealers.Add(this);
				ctx.SaveChanges();
				System.IO.File.WriteAllText(@"C:\ZDrive\hetulDebug.txt", "Dealer has been called");
			}
		}

		public Dealer()
		{

		}
	}

	public class Manufacturer
	{
		public int ManufacturerID { get; set; }
		public string Name { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public string Address { get; set; }

		public Manufacturer(string name, string phone, string email, string address)
		{
			this.Name = name;
			this.Phone = phone;
			this.Email = email;
			this.Address = address;
			int max;
			using (var ctx = new PharmaContext())
			{
				if (ctx.Manufacturers.Count() == 0)
				{
					max = 0;
				}
				else
				{
					max = (ctx.Manufacturers.Max(p => p.ManufacturerID));
				}
				this.ManufacturerID = max + 1;
				ctx.Manufacturers.Add(this);
				ctx.SaveChanges();
				System.Diagnostics.Debug.WriteLine("New Manufacturer");
			}
		}

		public Manufacturer()
		{

		}
	}

	public class Ledger
	{
		[Key]
		public int Entry { get; set; }
		public string Customer { get; set; }
		public Medicine Medicine { get; set; }
		public int Quantity { get; set; }
		public double Amount { get; set; }

		private Ledger(string customer, int medicineID, int quantity)
		{
			this.Customer = customer;
			this.Quantity = quantity;
			int max;
			using (var ctx = new PharmaContext())
			{
				this.Medicine = ctx.Medicines.Where(m => m.MedicineID == medicineID).FirstOrDefault();
				this.Amount = this.Medicine.Price * quantity;
				if (ctx.Ledgers.Count() == 0)
				{
					max = 0;
				}
				else
				{
					max = (ctx.Ledgers.Max(p => p.Entry));
				}
				this.Medicine.Stock -= quantity;
				ctx.Ledgers.Add(this);
				ctx.SaveChanges();
			}
			this.Entry = max + 1;
		}

		public Ledger()
		{

		}

		public static Ledger LedgerFactory(string customer, int medID, int quan)
		{
			Medicine fetched;
			using (var ctx = new PharmaContext())
			{
				fetched = ctx.Medicines.Where(m => m.MedicineID == medID).FirstOrDefault();
				if (fetched.Stock >= quan)
				{
					Ledger newEntry = new Ledger(customer, fetched.MedicineID, quan);
					return newEntry;
				}
				else
				{
					return null;
				}

			}
		}
	}
}