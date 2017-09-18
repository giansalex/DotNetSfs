/*
 * Enums de los diferentes valores que proporciona la SUNAT para estandarizar sus códigos de sus diversas operaciones.
 * Basado en el Anexo No 8 de su Manual Para la Facturación Electrónica.
 */

// ReSharper disable once CheckNamespace
namespace FacturacionElectronica.GeneradorXml.Enums
{

    /// <summary>
    /// Código de tipo de documento autorizado para efectos tributarios 
    /// cbc:InvoiceTypeCode 
    /// </summary>
    public enum TipoDocumentoElectronico
    {
        /// <summary>
        /// Factura
        /// </summary>
        Factura = 1,
        /// <summary>
        /// Boleta
        /// </summary>
        Boleta = 3,
        /// <summary>
        /// Nota de Credito
        /// </summary>
        NotaCredito = 7,
        /// <summary>
        /// Nota de Debito
        /// </summary>
        NotaDebito = 8,
        /// <summary>
        /// Guia de Remision Remitente
        /// </summary>
        GuiaRemisionRemitente = 9,
        /// <summary>
        /// Ticket de Maquina Registradora
        /// </summary>
        TicketMaquinaRegistradora = 12,
        /// <summary>
        /// DOCUMENTO EMITIDO POR BANCOS, INSTITUCIONES FINANCIERAS, CREDITICIAS Y DE SEGUROS QUE SE ENCUENTREN BAJO EL CONTROL DE LA SUPERINTENDENCIA DE BANCA Y SEGUROS
        /// </summary>
        DocumentoBanco = 13,
        /// <summary>
        /// DOCUMENTOS EMITIDOS POR LAS AFP
        /// </summary>
        DocumentoAfp = 18,
        /// <summary>
        /// Guia de Remision Transportista.
        /// </summary>
        GuiaRemisionTransportista = 31,
        /// <summary>
        /// COMPROBANTE DE PAGO SEAE
        /// </summary>
        ComprobanteSeae = 56,
        /// <summary>
        /// GUIA DE REMISIÓN REMITENTE COMPLEMENTARIA
        /// </summary>
        GRemisionRemitenteComp = 71,
        /// <summary>
        /// GUIA DE REMISION TRANSPORTISTA COMPLEMENTARIA
        /// </summary>
        GRemisionTransportistaComp = 72,
        /// <summary>
        /// COMPROBANTE DE RETENCION
        /// </summary>
        ComprobanteRetencion = 20,
        /// <summary>
        /// COMPROBANTE DE PERCEPCION
        /// </summary>
        ComprobantePercepcion = 40,
        /// <summary>
        /// COMPROBANTE DE PERCEPCION - VENTA INTERNA(FISICO - FORMATO IMPRESO).
        /// </summary>
        ComprobPercVentaInterna = 41
    }

    /// <summary>
    /// Tipo de Documento de Identificación 
    /// cbc:AdditionalAccountID 
    /// </summary>
    public enum TipoDocumentoIdentidad
    {
        /// <summary>
        /// DOC.TRIB.NO.DOM.SIN.RUC
        /// </summary>
        NoDomiciliado = 0,
        /// <summary>
        /// DOC. NACIONAL DE IDENTIDAD
        /// </summary>
        DocumentoNacionalIdentidad = 1,
        /// <summary>
        /// CARNET DE EXTRANJERIA
        /// </summary>
        CarnetExtranjeria = 4,
        /// <summary>
        /// REG. UNICO DE CONTRIBUYENTES
        /// </summary>
        RegistroUnicoContribuyentes = 6,
        /// <summary>
        /// PASAPORTE
        /// </summary>
        Pasaporte = 7
    }

    /// <summary>
    /// Tipo de Afectación al IGV
    /// cbc:TaxExemptionReasonCode 
    /// </summary>
    public enum TipoAfectacionIgv
    {
        /// <summary>
        /// Gravado -  Operación Onerosa
        /// </summary>
        GravadoOperacionOnerosa = 10,
        /// <summary>
        /// Gravado – Retiro por premio
        /// </summary>
        GravadoRetiroPorPremio = 11,
        /// <summary>
        /// Gravado – Retiro por donación
        /// </summary>
        GravadoRetiroPorDonacion = 12,
        /// <summary>
        /// Gravado – Retiro 
        /// </summary>
        GravadoRetiro = 13,
        /// <summary>
        /// Gravado – Retiro por publicidad
        /// </summary>
        GravadoRetiroPorPublicidad = 14,
        /// <summary>
        /// Gravado – Bonificaciones
        /// </summary>
        GravadoBonificaciones = 15,
        /// <summary>
        /// Gravado – Retiro por entrega a trabajadores
        /// </summary>
        GravadoRetiroEntregaTrabajadores = 16,
        /// <summary>
        /// Gravado – IVAP
        /// </summary>
        GravadoIvap = 17,
        /// <summary>
        /// Exonerado - Operación Onerosa
        /// </summary>
        ExoneradoOperacionOnerosa = 20,
        /// <summary>
        /// Exonerado – Transferencia Gratuita
        /// </summary>
        ExoneradoTransfGratuita = 21,
        /// <summary>
        /// Inafecto - Operación Onerosa 
        /// </summary>
        InafectoOperacionOnerosa = 30,
        /// <summary>
        /// Inafecto – Retiro por Bonificación
        /// </summary>
        InafectoPorBonificacion = 31,
        /// <summary>
        /// Inafecto – Retiro
        /// </summary>
        InafectoRetiro = 32,
        /// <summary>
        /// Inafecto – Retiro por Muestras Médicas
        /// </summary>
        InafectoMuestrasMedicas = 33,
        /// <summary>
        /// Inafecto -  Retiro por Convenio Colectivo
        /// </summary>
        InafectoConvenioColectivo = 34,
        /// <summary>
        /// Inafecto – Retiro por premio
        /// </summary>
        InafectoRetiroPorPremio = 35,
        /// <summary>
        /// Inafecto -  Retiro por publicidad
        /// </summary>
        InafectoRetiroPorPublicidad = 36,
        /// <summary>
        /// Exportación
        /// </summary>
        Exportacion = 40
    }

    /// <summary>
    /// Códigos de Tipos de Sistema de Cálculo del ISC
    /// </summary>
    public enum TipoSistemaIsc
    {
        /// <summary>
        /// Sistema al valo
        /// </summary>
        SistemValor = 1,
        /// <summary>
        /// Aplicación del Monto Fijo
        /// </summary>
        AplicacionMontoFijo,
        /// <summary>
        /// Sistema de Precios de Venta al Público
        /// </summary>
        PrecioVentaPublico
    }

    /// <summary>
    /// Código de tipo de tributo 
    /// cbc:TaxTypeCode 
    /// </summary>
    public enum TipoTributo
    {
        /// <summary>
        /// IGV IMPUESTO GENERAL A LAS VENTAS 
        /// </summary>
        IGV_VAT = 1000,
        /// <summary>
        /// ISC IMPUESTO SELECTIVO AL CONSUMO
        /// </summary>
        ISC_EXC = 2000,
        /// <summary>
        /// OTROS CONCEPTOS DE PAGO
        /// </summary>
        OTROS_OTH = 9999
    }

    /// <summary>
    /// Tipo de nota de crédito electrónica según motivo 
    /// cbc:ResponseCode 
    /// </summary>
    public enum TipoNotaCreditoElectronica
    {
        /// <summary>
        /// Motivo a utilizarse cuando se produce una anulación total de la venta de bienes o la anulación total o parcial de los servicios no ejecutados.
        /// </summary>
        AnulacionOperacion = 1,
        /// <summary>
        /// Anulación del comprobante de pago electrónico cuando este ha sido emitido a un sujeto distinto del adquirente o usuario.
        /// </summary>
        AnulacionErrorRuc = 2,
        /// <summary>
        /// Emision de nota de crédito para corregir un comprobante de pago electrónico que contenga una descripción que no corresponde al bien vendido 
        /// o cedido en uso o al tipo de servicio prestado.
        /// </summary>
        CorrecionPorErrorDescripcion = 3,
        /// <summary>
        /// Motivo a utilizarse cuando se aplique descuentos a los importes totales de una factura electrónica.
        /// </summary>
        DescuentoGlobal = 4,
        /// <summary>
        /// Motivo a utilizarse cuando se aplique descuentos a determinado(s) ítem(s) de una factura electrónica.
        /// </summary>
        DescuentoPorItem = 5,
        /// <summary>
        /// Cuando la emisión de la nota de crédito sea para documentar la devolución de los bienes facturados.
        /// </summary>
        DevolucionTotal = 6,
        /// <summary>
        /// Cuando la emisión de la nota de crédito sea para documentar la devolución de parte de los bienes facturados.
        /// </summary>
        DevolucionPorItem = 7,
        /// <summary>
        /// Corresponde utilizar este motivo de la nota de crédito tratándose de bonificaciones efectuadas 
        /// con posterioridad a la emisión del comprobante de pago que respalda la adquisición de bienes.
        /// </summary>
        Bonificacion = 8,
        /// <summary>
        /// Motivo a ser utilizado para ajustar el valor inicialmente facturado por otras circunstancias.
        /// </summary>
        DisminucionEnElValor = 9,
        /// <summary>
        /// Otros Conceptos 
        /// </summary>
        OtrosConceptos = 10
    }

    /// <summary>
    /// Tipo de nota de débito electrónica según motivo 
    /// cbc:ResponseCode 
    /// </summary>
    public enum TipoNotaDebitoElectronica
    {
        /// <summary>
        /// Intereses por mora
        /// </summary>
        InteresesPorMora = 1,
        /// <summary>
        /// Aumento en el valor
        /// </summary>
        AumentoEnElValor = 2,
        /// <summary>
        /// Penalidades/ otros conceptos 
        /// </summary>
        OtrosConceptos = 3
    }

    /// <summary>
    /// Tipo de importe de valor de venta registrado en el Resumen diario 
    /// cbc:InstructionID 
    /// </summary>
    public enum TipoValorVenta
    {
        /// <summary>
        /// Gravado
        /// </summary>
        Gravado = 1,
        /// <summary>
        /// Exonerado
        /// </summary>
        Exonerado,
        /// <summary>
        /// Inafecto
        /// </summary>
        Inafecto,
        /// <summary>
        /// Exportacion
        /// </summary>
        Exportacion,
        /// <summary>
        /// Gratuitas
        /// </summary>
        Gratuitas
    }

    /// <summary>
    /// Códigos de otros conceptos tributarios 
    /// ext:UBLExtensions/ext:UBLExtension/ext:ExtensionContent/sac:AdditionalInformationInvoice/sac:AdditionalMonetaryTotal/cbc:ID 
    /// </summary>
    public enum OtrosConceptosTributarios
    {
        /// <summary>
        /// Total valor de venta - operaciones gravadas
        /// </summary>
        TotalVentaOperacionesGravadas = 1001,
        /// <summary>
        /// Total valor de venta - operaciones inafectas
        /// </summary>
        TotalVentaOperacionesInafectas = 1002,
        /// <summary>
        /// Total valor de venta - operaciones exoneradas
        /// </summary>
        TotalVentaOperacionesExoneradas = 1003,
        /// <summary>
        /// Total valor de venta – Operaciones gratuitas
        /// </summary>
        TotalVentaOperacionesGratuitas = 1004,
        /// <summary>
        /// Sub total de venta
        /// </summary>
        SubTotalVenta = 1005,
        /// <summary>
        /// Percepciones
        /// </summary>
        Percepciones = 2001,
        /// <summary>
        /// Retenciones
        /// </summary>
        Retenciones = 2002,
        /// <summary>
        /// Detracciones
        /// </summary>
        Detracciones = 2003,
        /// <summary>
        /// Bonificaciones
        /// </summary>
        Bonificaciones = 2004,
        /// <summary>
        /// Total descuentos
        /// </summary>
        TotalDescuentos = 2005,
        /// <summary>
        /// FISE (Ley 29852) Fondo Inclusión Social Energético
        /// </summary>
        FondoInclusionSocial = 3001
    }

    /// <summary>
    /// Tipo de precio de venta 
    /// cac:AlternativeConditionPrice/cbc:PriceTypeCode 
    /// </summary>
    public enum TipoPrecioVenta
    {
        /// <summary>
        /// Precio unitario (incluye el IGV) 
        /// </summary>
        PrecioUnitario = 1,
        /// <summary>
        /// Valor referencial unitario en operaciones no onerosas 
        /// </summary>
        ValorReferencial = 2
    }

    /// <summary>
    /// Código de estado del ítem para Resumen de Boletas y sus Notas Electronicas Asociadas
    /// </summary>
    public enum EstadoResumen
    {
        /// <summary>
        /// Adicionar.
        /// </summary>
        Adicionar = 1,
        /// <summary>
        /// Modificar.
        /// </summary>
        Modificar,
        /// <summary>
        /// Anulado.
        /// </summary>
        Anulado,
        /// <summary>
        /// Anulado en el Dia (anulado antes de informar comprobante) Transport Publico.
        /// </summary>
        AnuladoDia
    }
}
