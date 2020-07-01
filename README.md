# Pharmacy Management System Service

Pharmacy management system is a Windows Communication Foundation based software used to manage commercial overhead of any pharmaceutical store by easing out stock and contact management as well as management of ledger for the same.

A pharmacist can login into the system after registering. He/she can then
add or remove manufacturers, dealers whom he/she is associated with. The listing
for the same is also provided. He/she can event add or remove the stock of all
sorts of medicine in his inventory.

Billing feature is also provided using which whenever a customer buys any
medicine, not only it gets reflected into the ledger, but the necessary updates are
also carried out in the stock. 

Technologies:
 - Windows Forms
 - Windows Communication Foundation
 - .NET Framework
Tools
 - Visual Studio 2019
Platforms
 - x86/x86-64 Windows OS 

PS: The project got deleted by mistake while transferring. I tried to recover the project using Recuva, but could't recover 2 files out of which one was containing code (Service.cs). IService.cs being the contract file contains all the method signatures which were present in Service.cs. Most of the business logic and design patterns are implemented in PharmaContext.cs. Thus, with Service.cs only a small amount of less significant code has perished viz., instantiating and calling methods of DbContext class to CRUD according to the contract specified in IService.cs
