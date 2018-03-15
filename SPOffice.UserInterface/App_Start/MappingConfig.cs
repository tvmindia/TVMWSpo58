using UserInterface.Models;
using SPOffice.DataAccessObject.DTO;
using SAMTool.DataAccessObject.DTO;
using SPOffice.UserInterface.Models;

namespace UserInterface.App_Start
{
    public class MappingConfig
    {
        public static void RegisterMaps()
        {
            AutoMapper.Mapper.Initialize(config =>
            {
                //domain <===== viewmodel
                //viewmodel =====> domain
                //ReverseMap() makes it possible to map both ways.


                //*****SAMTOOL MODELS 
                config.CreateMap<LoginViewModel, SAMTool.DataAccessObject.DTO.User>().ReverseMap();
                config.CreateMap<UserViewModel, SAMTool.DataAccessObject.DTO.User>().ReverseMap();
                config.CreateMap<SysMenuViewModel, SAMTool.DataAccessObject.DTO.SysMenu>().ReverseMap();
                config.CreateMap<RolesViewModel, SAMTool.DataAccessObject.DTO.Roles>().ReverseMap();
                config.CreateMap<ApplicationViewModel, SAMTool.DataAccessObject.DTO.Application>().ReverseMap();
                config.CreateMap<AppObjectViewModel, SAMTool.DataAccessObject.DTO.AppObject>().ReverseMap();
                config.CreateMap<AppSubobjectViewmodel, SAMTool.DataAccessObject.DTO.AppSubobject>().ReverseMap();
                config.CreateMap<CommonViewModel, SAMTool.DataAccessObject.DTO.Common>().ReverseMap();
                config.CreateMap<ManageAccessViewModel, SAMTool.DataAccessObject.DTO.ManageAccess>().ReverseMap();
                config.CreateMap<ManageSubObjectAccessViewModel, SAMTool.DataAccessObject.DTO.ManageSubObjectAccess > ().ReverseMap();
                config.CreateMap<PrivilegesViewModel, SAMTool.DataAccessObject.DTO.Privileges>().ReverseMap();

                
                //****SAMTOOL MODELS 




                //****SPOffice MODELS 
                config.CreateMap<MenuViewModel, Menu>().ReverseMap();
                
                config.CreateMap<CommonViewModel, SPOffice.DataAccessObject.DTO.Common>().ReverseMap();
                config.CreateMap<EnquiryViewModel, Enquiry>().ReverseMap();
                config.CreateMap<DashboardStatusViewModel, DashboardStatus>().ReverseMap();
                config.CreateMap <FollowUpViewModel, FollowUp>().ReverseMap();
                config.CreateMap<QuotationViewModel, Quotation>().ReverseMap();
                config.CreateMap<ProformaInvoiceViewModel, ProformaInvoice>().ReverseMap();
                config.CreateMap<ProformaViewModel, Proforma>().ReverseMap();
                config.CreateMap<CustomerPOViewModel, CustomerPO>().ReverseMap();
                config.CreateMap<SupplierViewModel, Supplier>().ReverseMap();
                config.CreateMap<SuppliersViewModel, Suppliers>().ReverseMap();
                config.CreateMap<SupplierOrderViewModel, SupplierOrder>().ReverseMap();

                config.CreateMap<IndustryViewModel, Industry>().ReverseMap();
                config.CreateMap<EnquirySourceViewModel, EnquirySource>().ReverseMap();
                config.CreateMap<EnquiryStatusViewModel, EnquiryStatus>().ReverseMap();
                config.CreateMap<ProgressStatusViewModel, ProgressStatus>().ReverseMap();
                config.CreateMap<TitlesViewModel, Titles>().ReverseMap();
                config.CreateMap<ReminderViewModel, Reminder>().ReverseMap();
                config.CreateMap<PriorityViewModel, Priority>().ReverseMap();



                config.CreateMap<ProductViewModel, Product>().ReverseMap();
                config.CreateMap<UnitViewModel, Unit>().ReverseMap();
                config.CreateMap<CourierAgencyViewModel, CourierAgency>().ReverseMap();
                config.CreateMap<RawMaterialViewModel, RawMaterial>().ReverseMap();
                config.CreateMap<QuoteHeaderViewModel, QuoteHeader>().ReverseMap();               
                config.CreateMap<QuoteItemViewModel, QuoteItem>().ReverseMap();
                config.CreateMap<CompanyViewModel, Company>().ReverseMap();
                config.CreateMap<QuoteStageViewModel, QuoteStage>().ReverseMap();
                config.CreateMap<CustomerViewModel, Customer>().ReverseMap();
                config.CreateMap<EmployeeViewModel, Employee>().ReverseMap();
                config.CreateMap<EmployeeTypeViewModel, EmployeeType>().ReverseMap();
                config.CreateMap<EmployeeCategoryViewModel, EmployeeCategory>().ReverseMap();
                config.CreateMap<SalesPersonViewModel, SalesPerson>().ReverseMap();
                config.CreateMap<TaxTypeViewModel, TaxType>().ReverseMap();
                config.CreateMap<CourierViewModel, Courier>().ReverseMap();
                config.CreateMap<PurchaseOrderStatusViewModel, PurchaseOrderStatus>().ReverseMap();
                config.CreateMap<EnquirySummaryViewModel, EnquirySummary>().ReverseMap();
                config.CreateMap<CustomerPOSummaryViewModel, CustomerPOSummary>().ReverseMap();
                config.CreateMap<QuotationSummaryViewModel, QuotationSummary>().ReverseMap();

                config.CreateMap<ProformaHeaderViewModel, ProformaHeader>().ReverseMap();
                config.CreateMap<ProformaItemViewModel, ProformaItem>().ReverseMap();
                config.CreateMap<ProformaStageViewModel, ProformaStage>().ReverseMap();
                config.CreateMap<ProformaInvoiceSummaryViewModel, ProformaInvoiceSummary>().ReverseMap();
                config.CreateMap<RequisitionViewModel, Requisition>().ReverseMap();
                config.CreateMap<RequisitionDetailViewModel, RequisitionDetail>().ReverseMap();
                config.CreateMap<UnitsViewModel, Units>().ReverseMap();
                config.CreateMap<POStatusesViewModel, POStatuses>().ReverseMap();
                config.CreateMap<SupplierPODetailViewModel, SupplierPODetail>().ReverseMap();
                config.CreateMap<ReqDetailViewModel, ReqDetail>().ReverseMap();
                config.CreateMap<ReqDetailLinkViewModel, ReqDetailLink>().ReverseMap();
                config.CreateMap<SupplierPOMailPreviewViewModel, SupplierPOMailPreview>().ReverseMap();

                //--For Report purpose
                config.CreateMap<ReportViewModel, Reports>().ReverseMap();
                config.CreateMap<EnquiryReport, EnquiryReportViewModel>().ReverseMap();

                config.CreateMap<RequisitionOverViewCountViewModel, RequisitionOverViewCount>().ReverseMap();
                config.CreateMap<CourierReportViewModel, CourierReport>().ReverseMap();
                config.CreateMap<QuotationReport, QuotationReportViewModel>().ReverseMap();
                config.CreateMap<CustomerPOReportViewModel, CustomerPOReport>().ReverseMap();
                config.CreateMap<RequisitionReportViewModel, RequisitionReport>().ReverseMap();
                config.CreateMap<ReqAdvanceSearchViewModel, ReqAdvanceSearch>().ReverseMap();
                //--for send message
                config.CreateMap<MessageViewModel, Message>().ReverseMap();
                config.CreateMap<EnquiryItemViewModel, EnquiryItem>().ReverseMap();
                config.CreateMap<MaterialTypeViewModel, MaterialType>().ReverseMap();
            });
        }
    }
}