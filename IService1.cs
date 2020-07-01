using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCFPharma
{
	[ServiceContract]
	public interface IService1
	{
		[OperationContract]
		bool CheckLogin(int uname, string password);

		[OperationContract]
		ListMedicines GetAllMedicines();
		
		[OperationContract]
		ListDealers GetAllDealers();

		[OperationContract]
		void SetDealer(string n, string p, string e, string a);
		
		[OperationContract]
		ListManufacturers GetAllManufacturers();

		[OperationContract]
		void SetManufacturer(string n, string p, string e, string a);

		[OperationContract]
		void SetMedicine(string name, string price, string stock, string delid, string manid);

		[OperationContract]
		bool SetLedger(string customer, string medicineID, string quantity);

		[OperationContract]
		ListLedger GetAllLedgers();

		[OperationContract]
		bool DeleteContract(int entity, int id);

		[OperationContract]
		bool UpdateStock(int id, int quantity);
	}

	[DataContract]
	public class ListMedicines
	{
		[DataMember]
		public List<Medicine> medi { get; set; }
	}

	[DataContract]
	public class ListDealers
	{
		[DataMember]
		public List<Dealer> dels { get; set; }
	}

	[DataContract]
	public class ListManufacturers
	{
		[DataMember]
		public List<Manufacturer> mans { get; set; }
	}

	[DataContract]
	public class ListLedger
	{
		[DataMember]
		public List<Ledger> ledg { get; set; }
	}
}
