using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Ubl.V2.Pe.common;
using Ubl.V2.Pe.maindoc;
using Xunit;

namespace DotNetSfs.Xml.Tests
{
    public class XmlOtrosCeGeneratorTests
    {
        private readonly XmlOtrosCeGenerator _generator;

        public XmlOtrosCeGeneratorTests()
        {
            var filename = Path.Combine(Environment.CurrentDirectory, "Resources", "SFSCert.p");
            var certify = new X509Certificate2(File.ReadAllBytes(filename), "123456");
            _generator = new XmlOtrosCeGenerator(certify);
        }

        [Fact]
        public void GenerateDocGuiaTest()
        {
            var guia = CreateGuia();
            var res = _generator.GenerateDocGuia(guia);

            Assert.NotNull(res);
            Assert.True(res.Success);
            Assert.NotNull(res.Content);
            Assert.True(res.Content.Length > 0);
            File.WriteAllBytes(Path.Combine(Path.GetTempPath(), res.FileName + ".xml"), res.Content);
        }

        private DespatchAdviceType CreateGuia()
        {
            return new DespatchAdviceType
            {
                UBLVersionID = "2.1",
                UBLExtensions = new[] { new UBLExtensionType { ExtensionContent = new AdditionalsInformationType() }, },
                ID = "T001-00000001",
                CustomizationID = "1.0",
                DespatchAdviceTypeCode = "09",
                IssueDate = DateTime.Now,
                Signature = new[]
                {
                    new SignatureType
                    {
                        ID = "IDSignCR",
                        SignatoryParty = new PartyType
                        {
                            PartyIdentification = new[] { new PartyIdentificationType { ID = "20600995805" } },
                            PartyName = new[] { new PartyNameType { Name = "EMPRESA SAC" } }
                        },
                        DigitalSignatureAttachment = new AttachmentType { ExternalReference = new ExternalReferenceType { URI = "#SignatureSP" } }
                    }
                },
                Note = new TextType[]
                {
                    "Transporto bolsas para basura"
                },
                OrderReference = new[]
                {
                    new OrderReferenceType
                    {
                        ID = "T001-8",
                        OrderTypeCode = "09"
                    }
                },
                DespatchSupplierParty = new SupplierPartyType()
                {
                    CustomerAssignedAccountID = new IdentifierType
                    {
                        schemeID = "6",
                        Value = "20600995805"
                    },
                    Party = new PartyType
                    {
                        PartyLegalEntity = new[] { new PartyLegalEntityType { RegistrationName = "EMPRESA SAC" } }
                    }
                },
                DeliveryCustomerParty = new CustomerPartyType()
                {
                    CustomerAssignedAccountID = new IdentifierType
                    {
                        schemeID = "6",
                        Value = "20100070970"
                    },
                    Party = new PartyType
                    {
                        PartyLegalEntity = new[] { new PartyLegalEntityType
                            {
                                RegistrationName = "EMPRESA ABC"
                            }
                        }
                    }
                },
                SellerSupplierParty = new SupplierPartyType
                {
                    CustomerAssignedAccountID = new IdentifierType
                    {
                        Value = "20100070970",
                        schemeID = "6"
                    },
                    Party = new PartyType
                    {
                        PartyLegalEntity = new[]
                        {
                            new PartyLegalEntityType
                            {
                                RegistrationName = "PLAZA VEA"
                            }
                        }
                    }
                },
                Shipment = new ShipmentType
                {
                    ID = "1",
                    HandlingCode = "01",
                    Information = "VENTA",
                    SplitConsignmentIndicator = true,
                    GrossWeightMeasure = new MeasureType
                    {
                        Value = 10000.00M,
                        unitCode = "KGM"
                    },
                    TotalTransportHandlingUnitQuantity = 5,
                    ShipmentStage = new[]
                    {
                        new ShipmentStageType
                        {
                            TransportModeCode  = "01", // Transporte Public
                            TransitPeriod = new PeriodType
                            {
                                StartDate = new DateTime(2017, 7, 1),
                            },
                            CarrierParty = new []
                            {
                                new PartyType
                                {
                                    PartyIdentification = new []
                                    {
                                        new PartyIdentificationType
                                        {
                                            ID = new IdentifierType
                                            {
                                                schemeID = "6",
                                                Value = "20100070973"
                                            }
                                        }
                                    },
                                    PartyName = new []
                                    {
                                        new PartyNameType
                                        {
                                            Name = "TRANSPORTISTA"
                                        }
                                    }
                                }
                            },
                            TransportMeans = new TransportMeansType
                            {
                                RoadTransport = new RoadTransportType
                                {
                                    LicensePlateID = "PGY-0988"
                                }
                            },
                            DriverPerson = new PartyIdentificationType
                            {
                                ID = new IdentifierType
                                {
                                    schemeID = "1",
                                    Value = "10101010"
                                }
                            }
                        }
                    },
                    Delivery = new DeliveryType
                    {
                        DeliveryAddress = new AddressType
                        {
                            ID = "120606",
                            StreetName = "JR. MANTARO NRO. 257"
                        }
                    },
                    TransportHandlingUnit = new[]
                    {
                        new TransportHandlingUnitType
                        {
                            ID = "PGY-0988",
                            TransportEquipment = new []
                            {
                                new TransportEquipmentType
                                {
                                    ID = "PGY-645"
                                }
                            }
                        }
                    },
                    OriginAddress = new AddressType
                    {
                        ID = "150123", // 8 characters segun Guia
                        StreetName = "CAR. PANAM SUR KM 25 NO. 25050 NRO. 050 Z.I. CONCHAN"
                    },
                    FirstArrivalPortLocation = new LocationType1
                    {
                        ID = "PAI"
                    }
                },
                DespatchLine = new[]
                {
                    new DespatchLineType
                    {
                        ID = "1",
                        DeliveredQuantity  = new QuantityType
                        {
                            Value = 10M,
                            unitCode = "KGM"
                        },
                        OrderLineReference   = new []
                        {
                            new OrderLineReferenceType
                            {
                                LineID = "1" // ID
                            }
                        },
                        Item = new ItemType
                        {
                            Name = "ACETONA - 500.50 BALDE - 500.50 BALAS",
                            SellersItemIdentification = new ItemIdentificationType
                            {
                                ID = "COD1"
                            }
                        }
                    }
                }
            };
        }
    }
}