using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using DotNetSfs.Xml.Entity;
using DotNetSfs.Xml.Res;
using Ubl.V2.Pe.common;
using Ubl.V2.Pe.maindoc;

namespace DotNetSfs.Xml
{
    /// <summary>
    /// Class para generar documentos XML , firmados cumpliendo el Estandar UBL 2.0 requerido por SUNAT
    /// <remarks>Invoice, Summary, Voided</remarks>
    /// </summary>
    public class XmlDocGenerator
    {
        #region Field
        private readonly X509Certificate2 _certificado;
        #endregion

        #region Construct
        /// <summary>
        /// Crea una Instancia de <c>XmlDocGenerator</c>.
        /// </summary>
        /// <param name="certificado">Certificado X509 v3 con el que se firmara</param>
        public XmlDocGenerator(X509Certificate2 certificado)
        {
            _certificado = certificado;
        }
        #endregion

        #region Method
        /// <summary>
        /// Genera un documento XML para la emision de un comprobante.
        /// </summary>
        /// <param name="invoiceHeaderEntity">Entidad Invoice</param>
        /// <returns>Retorna el XML generado.</returns>
        public XmlFileResult GeneraDocumentoInvoice(InvoiceHeader invoiceHeaderEntity)
        {
            try
            {
                #region Filename
                var xmlFilename =
                    $"{invoiceHeaderEntity.RucEmisor}-{(int) invoiceHeaderEntity.TipoDocumento:00}-" +
                    $"{invoiceHeaderEntity.SerieDocumento}-{invoiceHeaderEntity.CorrelativoDocumento}";
                #endregion

                #region Gen Invoice
                AmountType.TlsDefaultCurrencyID = invoiceHeaderEntity.CodigoMoneda;

                var invoice = new InvoiceType
                {
                    ID = $"{invoiceHeaderEntity.SerieDocumento}-{invoiceHeaderEntity.CorrelativoDocumento}",
                    IssueDate = invoiceHeaderEntity.FechaEmision,
                    InvoiceTypeCode = ((int)invoiceHeaderEntity.TipoDocumento).ToString("00"),
                    DocumentCurrencyCode = invoiceHeaderEntity.CodigoMoneda,
                    CustomizationID = "1.0",
                    DespatchDocumentReference = UtilsXmlDoc.DevuelveGuiasRemisionReferenciadas(invoiceHeaderEntity.GuiaRemisionReferencia),
                    AdditionalDocumentReference = UtilsXmlDoc.DevuelveDocumentosReferencia(invoiceHeaderEntity),
                    UBLExtensions = new[]
                    {    new UBLExtensionType
                        {
                            ExtensionContent = new AdditionalsInformationType //(mas informacion en el catalogo no 14 del manual.)
                                {
                                    AdditionalInformation = new AdditionalInformationType{
                                        AdditionalMonetaryTotal = UtilsXmlDoc.DevuelveTributosAdicionales(invoiceHeaderEntity.TotalTributosAdicionales),
                                        AdditionalProperty = invoiceHeaderEntity.InfoAddicional.ToArray(),
                                        SUNATEmbededDespatchAdvice = invoiceHeaderEntity.GuiaEmbebida,
                                        SUNATTransaction = invoiceHeaderEntity.TipoOperacion.HasValue ? new SUNATTransactionType
                                        {
                                            ID = ((int)invoiceHeaderEntity.TipoOperacion).ToString("00")
                                        }: null 
                                    }
                                }
                        },
                        new UBLExtensionType
                        {
                            ExtensionContent = new AdditionalsInformationType()
                        }
                    },
                    Signature = UtilsXmlDoc.GetSignature(invoiceHeaderEntity),
                    AccountingSupplierParty =UtilsXmlDoc.GetInfoEmisor(invoiceHeaderEntity),
                    AccountingCustomerParty = new CustomerPartyType
                    {
                        CustomerAssignedAccountID = invoiceHeaderEntity.NroDocCliente,
                        AdditionalAccountID =
                            new IdentifierType[] { ((int)invoiceHeaderEntity.TipoDocumentoIdentidadCliente).ToString() },
                        Party = new PartyType
                        {
                            PartyLegalEntity = new[]
                            {
                                new PartyLegalEntityType
                                {
                                    RegistrationName = invoiceHeaderEntity.NombreRazonSocialCliente,
                                }
                            }
                        },
                    },
                    PrepaidPayment = UtilsXmlDoc.GetAnticipos(invoiceHeaderEntity.Anticipos),
                    LegalMonetaryTotal = new MonetaryTotalType
                    {
                        AllowanceTotalAmount = invoiceHeaderEntity.DescuentoGlobal > 0 ? new AmountType {Value = invoiceHeaderEntity.DescuentoGlobal} : null,
                        PrepaidAmount = invoiceHeaderEntity.TotalAnticipos.HasValue ? new AmountType {Value = invoiceHeaderEntity.TotalAnticipos.Value } : null,
                        PayableAmount = invoiceHeaderEntity.TotalVenta
                    },
                    InvoiceLine = UtilsXmlDoc.DevuelveDetallesDelComprobante(invoiceHeaderEntity.DetallesDocumento),
                    TaxTotal = UtilsXmlDoc.DevuelveSubTotalImpuestos(invoiceHeaderEntity.Impuesto)
                };
                invoice.AccountingSupplierParty.Party.PostalAddress = UtilsXmlDoc.ObtenerDireccion(invoiceHeaderEntity.DireccionEmisor);
                #endregion

                return FromDocument(invoice, xmlFilename);
            }
            catch (Exception ex)
            {
                return FromException(ex);
            }
        }

        /// <summary>
        /// Genera un documento XML para Comunicacion de Baja.
        /// </summary>
        /// <param name="voidedHeaderEntity">Entidad Voided</param>
        /// <returns>Retorna el XML generado.</returns>
        public XmlFileResult GenerarDocumentoVoided(VoidedHeader voidedHeaderEntity)
        {
            try
            {
                #region Filename
                var id = $"RA-{DateTime.Today:yyyyMMdd}-{voidedHeaderEntity.CorrelativoArchivo}";
                var xmlFilename = voidedHeaderEntity.RucEmisor + "-" + id;
                #endregion

                #region Gen Voided
                var voidedDoc = new VoidedDocumentsType
                {
                    ID = id,
                    ReferenceDate = voidedHeaderEntity.FechaEmision,
                    CustomizationID = "1.0",
                    IssueDate = DateTime.Today.Date,
                    UBLExtensions = new[]
                    {
                        new UBLExtensionType
                        {
                            ExtensionContent = new AdditionalsInformationType()
                        },
                    },
                    Signature = UtilsXmlDoc.GetSignature(voidedHeaderEntity),
                    AccountingSupplierParty = UtilsXmlDoc.GetInfoEmisor(voidedHeaderEntity),
                    VoidedDocumentsLine = UtilsXmlDoc.GetVoidedLines(voidedHeaderEntity.DetallesDocumento)
                };
                #endregion

                return FromDocument(voidedDoc, xmlFilename);
            }
            catch (Exception ex)
            {
                return FromException(ex);
            }
        }

        /// <summary>
        /// Genera un documento XML para Resumen Diario.
        /// </summary>
        /// <param name="summaryHeaderEntity">Entidad de Resumen</param>
        /// <param name="version2">Es version 2</param>
        /// <returns>Retorna el XML generado.</returns>
        public XmlFileResult GenerarDocumentoSummary(SummaryHeader summaryHeaderEntity, bool version2 = false)
        {
            try
            {
                #region Filename
                string id = $"RC-{DateTime.Today:yyyyMMdd}-{summaryHeaderEntity.CorrelativoArchivo}";
                string xmlFilename = summaryHeaderEntity.RucEmisor + "-" + id;
                #endregion

                #region Gen Summary
                AmountType.TlsDefaultCurrencyID = summaryHeaderEntity.CodigoMoneda;
                var summaryDoc = new SummaryDocumentsType
                {
                    ID = id,
                    CustomizationID = version2 ? "1.1" : "1.0",
                    ReferenceDate = summaryHeaderEntity.FechaEmision,
                    IssueDate = DateTime.Today.Date,
                    UBLExtensions = new[]
                    {
                        new UBLExtensionType
                        {
                            ExtensionContent = new AdditionalsInformationType()
                        },
                    },
                    Signature = UtilsXmlDoc.GetSignature(summaryHeaderEntity),
                    AccountingSupplierParty = UtilsXmlDoc.GetInfoEmisor(summaryHeaderEntity),
                    SummaryDocumentsLine = UtilsXmlDoc.GetSummaryLines(summaryHeaderEntity.DetallesDocumento)
                };
                #endregion

                return FromDocument(summaryDoc, xmlFilename);
            }
            catch(Exception ex)
            {
                return FromException(ex);
            }
        }

        /// <summary>
        /// Genera un documento XML para Notas de Credito.
        /// </summary>
        /// <param name="creditHeaderEntity">Entidad de Nota de Credito</param>
        /// <returns>Retorna el XML generado.</returns>
        public XmlFileResult GenerarDocumentoCreditNote(CreditNoteHeader creditHeaderEntity)
        {
            try
            {
                #region FileName
                string xmlFilename =
                    $"{creditHeaderEntity.RucEmisor}-07-{creditHeaderEntity.SerieDocumento}-{creditHeaderEntity.CorrelativoDocumento}";
                #endregion

                #region Gen CreditNote
                AmountType.TlsDefaultCurrencyID = creditHeaderEntity.CodigoMoneda;
                var creditDoc = new CreditNoteType
                {
                    ID = string.Concat(creditHeaderEntity.SerieDocumento, "-", creditHeaderEntity.CorrelativoDocumento),
                    IssueDate = creditHeaderEntity.FechaEmision,
                    DocumentCurrencyCode = creditHeaderEntity.CodigoMoneda,
                    CustomizationID = "1.0",
                    DespatchDocumentReference = UtilsXmlDoc.DevuelveGuiasRemisionReferenciadas(creditHeaderEntity.GuiaRemisionReferencia),
                    AdditionalDocumentReference = UtilsXmlDoc.DevuelveDocumentosReferenciaNote(creditHeaderEntity),
                    DiscrepancyResponse = new[]
                    {
                        new ResponseType
                        {
                            ReferenceID = creditHeaderEntity.DocumentoRef,
                            ResponseCode = ((int)creditHeaderEntity.TipoNota).ToString("00"),
                            Description = new TextType[]
                            {
                                creditHeaderEntity.Motivo
                            }
                        }
                    },
                    BillingReference = new[]
                    {
                        new BillingReferenceType
                        {
                            InvoiceDocumentReference = new DocumentReferenceType
                            {
                                ID = creditHeaderEntity.DocumentoRef,
                                DocumentTypeCode = ((int)creditHeaderEntity.TipoDocRef).ToString("00")
                            }
                        }
                    },
                    Signature = UtilsXmlDoc.GetSignature(creditHeaderEntity),
                    AccountingSupplierParty = UtilsXmlDoc.GetInfoEmisor(creditHeaderEntity),
                    AccountingCustomerParty = new CustomerPartyType
                    {
                        CustomerAssignedAccountID = creditHeaderEntity.NroDocCliente,
                        AdditionalAccountID =
                                new IdentifierType[] { ((int)creditHeaderEntity.TipoDocumentoIdentidadCliente).ToString() },
                        Party = new PartyType
                        {
                            PartyLegalEntity = new[]
                                {
                                    new PartyLegalEntityType
                                    {
                                        RegistrationName = creditHeaderEntity.NombreRazonSocialCliente,
                                    }
                                }
                        },
                    },
                    TaxTotal = UtilsXmlDoc.DevuelveSubTotalImpuestos(creditHeaderEntity.Impuesto),
                    LegalMonetaryTotal = new MonetaryTotalType
                    {
                        ChargeTotalAmount = creditHeaderEntity.TotalCargos > 0 ? new AmountType { Value = creditHeaderEntity.TotalCargos } : null,
                        AllowanceTotalAmount = creditHeaderEntity.DescuentoGlobal > 0 ? new AmountType { Value = creditHeaderEntity.DescuentoGlobal } : null,
                        PayableAmount = creditHeaderEntity.Total
                    },
                    CreditNoteLine = UtilsXmlDoc.ToCredit(UtilsXmlDoc.DevuelveDetallesDelComprobante(creditHeaderEntity.DetallesDocumento)),
                };
                #region Ext
                var lisExt = new List<UBLExtensionType>(2);
                if (creditHeaderEntity.TotalTributosAdicionales != null)
                    lisExt.Add(new UBLExtensionType
                    {
                        ExtensionContent = new AdditionalsInformationType //(mas informacion en el catalogo no 14 del manual.)
                        { AdditionalInformation = new AdditionalInformationType { AdditionalMonetaryTotal = UtilsXmlDoc.DevuelveTributosAdicionales(creditHeaderEntity.TotalTributosAdicionales), } }
                    });
                lisExt.Add( new UBLExtensionType {ExtensionContent = new AdditionalsInformationType()
                });
                creditDoc.UBLExtensions = lisExt.ToArray();
                #endregion
                creditDoc.AccountingSupplierParty.Party.PostalAddress = UtilsXmlDoc.ObtenerDireccion(creditHeaderEntity.DireccionEmisor);
                #endregion|

                return FromDocument(creditDoc, xmlFilename);
            }
            catch (Exception ex)
            {
                return FromException(ex);
            }
        }

        /// <summary>
        /// Genera un documento XML para Notas de Debito.
        /// </summary>
        /// <param name="debitHeaderEntity">Entidad de Nota de Debito</param>
        /// <returns>Retorna el XML generado.</returns>
        public XmlFileResult GenerarDocumentoDebitNote(DebitNoteHeader debitHeaderEntity)
        {
            try
            {
                #region FileName
                string xmlFilename =
                    $"{debitHeaderEntity.RucEmisor}-08-{debitHeaderEntity.SerieDocumento}-{debitHeaderEntity.CorrelativoDocumento}";
                #endregion

                #region Gen DebitNote
                AmountType.TlsDefaultCurrencyID = debitHeaderEntity.CodigoMoneda;
                var debitDoc = new DebitNoteType
                {
                    ID = string.Concat(debitHeaderEntity.SerieDocumento, "-", debitHeaderEntity.CorrelativoDocumento),
                    IssueDate = debitHeaderEntity.FechaEmision,
                    DocumentCurrencyCode = debitHeaderEntity.CodigoMoneda,
                    CustomizationID = "1.0",
                    DespatchDocumentReference = UtilsXmlDoc.DevuelveGuiasRemisionReferenciadas(debitHeaderEntity.GuiaRemisionReferencia),
                    AdditionalDocumentReference = UtilsXmlDoc.DevuelveDocumentosReferenciaNote(debitHeaderEntity),
                    DiscrepancyResponse = new[]
                    {
                        new ResponseType
                        {
                            ReferenceID = debitHeaderEntity.DocumentoRef,
                            ResponseCode = ((int)debitHeaderEntity.TipoNota).ToString("00"),
                            Description = new TextType[]
                            {
                                debitHeaderEntity.Motivo
                            }
                        }
                    },
                    BillingReference = new[]
                    {
                        new BillingReferenceType
                        {
                            InvoiceDocumentReference = new DocumentReferenceType
                            {
                                ID = debitHeaderEntity.DocumentoRef,
                                DocumentTypeCode = ((int)debitHeaderEntity.TipoDocRef).ToString("00")
                            }
                        }
                    },
                    Signature = UtilsXmlDoc.GetSignature(debitHeaderEntity),
                    AccountingSupplierParty = UtilsXmlDoc.GetInfoEmisor(debitHeaderEntity),
                    AccountingCustomerParty = new CustomerPartyType
                    {
                        CustomerAssignedAccountID = debitHeaderEntity.NroDocCliente,
                        AdditionalAccountID =
                                new IdentifierType[] { ((int)debitHeaderEntity.TipoDocumentoIdentidadCliente).ToString() },
                        Party = new PartyType
                        {
                            PartyLegalEntity = new[]
                                {
                                    new PartyLegalEntityType
                                    {
                                        RegistrationName = debitHeaderEntity.NombreRazonSocialCliente,
                                    }
                                }
                        },
                    },
                    TaxTotal = UtilsXmlDoc.DevuelveSubTotalImpuestos(debitHeaderEntity.Impuesto),
                    RequestedMonetaryTotal = new MonetaryTotalType
                    {
                        ChargeTotalAmount = debitHeaderEntity.TotalCargos > 0 ? new AmountType { Value = debitHeaderEntity.TotalCargos } : null,
                        AllowanceTotalAmount = debitHeaderEntity.DescuentoGlobal > 0 ? new AmountType { Value = debitHeaderEntity.DescuentoGlobal } : null,
                        PayableAmount = debitHeaderEntity.Total
                    },
                    DebitNoteLine = UtilsXmlDoc.ToDebit(UtilsXmlDoc.DevuelveDetallesDelComprobante(debitHeaderEntity.DetallesDocumento)),
                };
                #region Ext
                var lisExt = new List<UBLExtensionType>(2);
                if (debitHeaderEntity.TotalTributosAdicionales != null)
                    lisExt.Add(new UBLExtensionType
                    {
                        ExtensionContent = new AdditionalsInformationType //(mas informacion en el catalogo no 14 del manual.)
                        { AdditionalInformation = new AdditionalInformationType { AdditionalMonetaryTotal = UtilsXmlDoc.DevuelveTributosAdicionales(debitHeaderEntity.TotalTributosAdicionales), } }
                    });
                lisExt.Add(new UBLExtensionType
                {
                    ExtensionContent = new AdditionalsInformationType()
                });
                debitDoc.UBLExtensions = lisExt.ToArray();
                #endregion
                debitDoc.AccountingSupplierParty.Party.PostalAddress = UtilsXmlDoc.ObtenerDireccion(debitHeaderEntity.DireccionEmisor);
                #endregion|

                return FromDocument(debitDoc, xmlFilename);
            }
            catch (Exception ex)
            {
                return FromException(ex);
            }
        }
        #endregion

        #region Internal

        private static XmlFileResult FromException(Exception er)
        {
            return new XmlFileResult
            {
                Error = er.Message
            };
        }

        private XmlFileResult FromDocument<TSunat>(TSunat doc, string filename)
             where TSunat : UblBaseDocumentType
        {
            var pobjOperationResult = new OperationResult();
            var ct = UtilsXmlDoc.GenFile(ref pobjOperationResult, doc, _certificado);

            var result = new XmlFileResult();

            if (pobjOperationResult.Success)
            {
                result.Success = true;
                result.FileName = filename;
                result.Content = ct;
            }
            else
                result.Error = pobjOperationResult.Error;

            return result;
        }
        #endregion
    }

    /// <summary>
    /// Generate Other Files - (CPE,CRE,CGR)
    /// </summary>
    public class XmlOtrosCeGenerator
    {
        #region Fields
        private readonly X509Certificate2 _certificado;
        private OperationResult _result;
        #endregion

        #region Properties

        /// <summary>
        /// Contiene Informacion de la Ultima Operacion.
        /// </summary>

        #endregion

        #region Construct
        /// <summary>
        /// Crea una Instancia de <see cref="XmlOtrosCeGenerator"/>
        /// </summary>
        /// <param name="certificado">Certificado X509 v3 con el que se firmara</param>
        public XmlOtrosCeGenerator(X509Certificate2 certificado)
        {
            _certificado = certificado;
            _result = new OperationResult();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Genera Documento de Retencion.
        /// </summary>
        /// <param name="doc">Retention UBL2.0</param>
        /// <returns>XML</returns>
        public XmlFileResult GenerateDocRetention(RetentionType doc)
        {
            var filename = $"{doc.AgentParty.PartyIdentification[0].ID.Value}-20-{doc.ID.Value}";
            return new XmlFileResult
            {
                Success = true,
                FileName = filename,
                Content = UtilsXmlDoc.GenFile(ref _result, doc, _certificado)
            };
        }

        /// <summary>
        /// Genera Documento de Perecepcion
        /// </summary>
        /// <param name="doc">Pereception UBL2.0</param>
        /// <returns>Path of XML</returns>
        public XmlFileResult GenerateDocPerception(PerceptionType doc)
        {
            var filename = $"{doc.AgentParty.PartyIdentification[0].ID.Value}-40-{doc.ID.Value}";
            return new XmlFileResult
            {
                Success = true,
                FileName = filename,
                Content = UtilsXmlDoc.GenFile(ref _result, doc, _certificado)
            };
        }

        /// <summary>
        /// Genera y Firma Documento de Guia de Remision.
        /// </summary>
        /// <param name="doc">Guia de Remision UBL2.1</param>
        /// <returns>XML</returns>
        public XmlFileResult GenerateDocGuia(DespatchAdviceType doc)
        {
            var filename = $"{doc.DespatchSupplierParty.CustomerAssignedAccountID.Value}-09-{doc.ID.Value}";
            return new XmlFileResult
            {
                Success = true,
                FileName = filename,
                Content = UtilsXmlDoc.GenFile(ref _result, doc, _certificado)
            };
        }

        /// <summary>
        /// Genera un documento XML para Resumen de Reversion.
        /// </summary>
        /// <param name="voidedHeaderEntity">Entidad Voided</param>
        /// <returns>XML</returns>
        public XmlFileResult GenerarDocumentoVoided(VoidedHeader voidedHeaderEntity)
        {
            try
            {
                #region Filename
                var id = $"RR-{DateTime.Today:yyyyMMdd}-{voidedHeaderEntity.CorrelativoArchivo}";
                var xmlFilename = voidedHeaderEntity.RucEmisor + "-" + id;
                #endregion

                #region Gen Voided
                var voidedDoc = new VoidedDocumentsType
                {
                    ID = id,
                    ReferenceDate = voidedHeaderEntity.FechaEmision,
                    CustomizationID = "1.0",
                    IssueDate = DateTime.Today.Date,
                    UBLExtensions = new[]
                    {
                        new UBLExtensionType
                        {
                            ExtensionContent = new AdditionalsInformationType()
                        },
                    },
                    Signature = UtilsXmlDoc.GetSignature(voidedHeaderEntity),
                    AccountingSupplierParty = UtilsXmlDoc.GetInfoEmisor(voidedHeaderEntity),
                    VoidedDocumentsLine = UtilsXmlDoc.GetVoidedLines(voidedHeaderEntity.DetallesDocumento)
                };
                #endregion

                return new XmlFileResult
                {
                    Success = true,
                    FileName = xmlFilename,
                    Content = UtilsXmlDoc.GenFile(ref _result, voidedDoc, _certificado)
                };
            }
            catch (Exception er)
            {
                _result.Success = false;
                _result.Error = er.Message;
                _result.Target = "XmlOtrosCeGenerator.GenerarDocumentoVoided()";
                return null;
            }
        }
        #endregion
    }
}
