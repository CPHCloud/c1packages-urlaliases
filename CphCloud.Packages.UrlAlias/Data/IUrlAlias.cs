using Composite.Data;
using Composite.Data.Hierarchy;
using Composite.Data.Hierarchy.DataAncestorProviders;
using System;
using Composite.Data.ProcessControlled.ProcessControllers.GenericPublishProcessController;
using Composite.Data.Validation.Validators;

namespace CphCloud.Packages.UrlAlias.Data
{
    [Title("Url Alias")]
    [AutoUpdateble]
    [DataAncestorProvider(typeof(NoAncestorDataAncestorProvider))]
    [KeyPropertyName("Id")]
    [LabelPropertyName("UrlAlias")]
    [DataScope(DataScopeIdentifier.PublicName)]
    [PublishProcessControllerType(typeof(GenericPublishProcessController))]
    [ImmutableTypeId("{723530BD-E79E-422A-B841-B3640D744AA9}")]
    public interface IUrlAlias : IData
    {

        [StoreFieldType(PhysicalStoreFieldType.Guid)]
        [ImmutableFieldId("{942BDDA5-A5EF-4E31-A5FF-85FBED905F12}")]
        Guid Id { get; set; }

        [StoreFieldType(PhysicalStoreFieldType.String, 64)]
        [ImmutableFieldId("{C7DEDEAA-686F-42BB-A546-AE8334EB2357}")]
        [StringSizeValidator(1, 64)]
        string UrlAlias { get; set; }

        [StoreFieldType(PhysicalStoreFieldType.String, 512)]
        [ImmutableFieldId("{C7830E71-BD2E-4CFB-BE9B-930E978F54E1}")]
        [StringSizeValidator(1, 512)]
        string RedirectLocation { get; set; }

        [StoreFieldType(PhysicalStoreFieldType.Integer)]
        [FunctionBasedNewInstanceDefaultFieldValue("<f:function xmlns:f=\"http://www.composite.net/ns/function/1.0\" name=\"Composite.Constant.Integer\"><f:param name=\"Constant\" value=\"302\" /></f:function>")]
        [ImmutableFieldId("{9374AE11-A986-4F08-823D-E5FFB8484415}")]
        int HttpStatusCode { get; set; }

        [StoreFieldType(PhysicalStoreFieldType.Boolean)]
        [FunctionBasedNewInstanceDefaultFieldValue("<f:function xmlns:f=\"http://www.composite.net/ns/function/1.0\" name=\"Composite.Constant.Boolean\"><f:param name=\"Constant\" value=\"True\" /></f:function>")]
        [ImmutableFieldId("{13B88A22-31CB-46AF-8152-2CCBAD232A99}")]
        bool Enabled { get; set; }

        [DefaultFieldGuidValue("00000000-0000-0000-0000-000000000000")]
        [ForeignKey("Composite.Data.Types.IHostnameBinding,Composite", AllowCascadeDeletes = true, NullReferenceValue = "{00000000-0000-0000-0000-000000000000}", NullReferenceValueType = typeof(Guid))]
        [StoreFieldType(PhysicalStoreFieldType.Guid)]
        [ImmutableFieldId("a57a50da-b24d-435d-a380-589a84ac7a75")]
        Guid Hostname { get; set; }

        [StoreFieldType(PhysicalStoreFieldType.String, 1024)]
        [ImmutableFieldId("{52EAA9B9-2D67-4E48-B31B-07010CAFD281}")]
        [StringSizeValidator(0, 1024)]
        string Note { get; set; }

    }
}
