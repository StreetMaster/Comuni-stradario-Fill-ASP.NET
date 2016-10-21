using System;

namespace FillWS_ASPNET
{
    /// <summary>
    /// Esempio di utilizzo del servizio WS FILL per la verifica e la normalizzazione degli indirizzi italiani 
    /// 
    /// L'end point del servizio è 
    ///     http://ec2-46-137-97-173.eu-west-1.compute.amazonaws.com/smws/fill?wsdl
    ///     
    /// Per l'utilizzo registrarsi sul sito http://streetmaster.it e richiedere la chiave per il servizio FILL 
    /// 
    ///  2016 - Software by StreetMaster (c)
    /// </summary>
    public partial class DemoFill : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCallVerify_Click(object sender, EventArgs e)
        {

            outArea.Style["Border"] = "none";
            outArea.Style["Border-color"] = "#336600";

            // oggetto client per l'utilizzo del ws FILL
            var fillObj = new FillWS.fill();

            // classe di input
            var inFill = new FillWS.inputCommon();

            // valorizzazione input
            inFill.cap = txtCap.Text;
            inFill.provincia = txtProv.Text;
            inFill.localita = txtComune.Text;
            inFill.localita2 = txtFrazione.Text;
            inFill.indirizzo = txtIndirizzo.Text;

            // chiamata al servizio
            var outCall = fillObj.Fill(inFill, txtKey.Text);

            
            // output
            if (outCall.norm==1)
            {
                // verifica OK
                txtCap.Text = outCall.outItem[0].cap;
                txtProv.Text= outCall.outItem[0].provincia;
                txtComune.Text = outCall.outItem[0].comune;
                txtFrazione.Text = outCall.outItem[0].frazione;
                txtIndirizzo.Text = outCall.outItem[0].indirizzo;
                string htmlOut = "<p><font color=\"green\">INDIRIZZO VALIDO</font></p>";

                // e' riportato in output solo un sottoinsieme di esempio di tutti i valori restituiti

                htmlOut += "<table>";

                htmlOut += "<tr><td>Regione</td>";
                htmlOut += "<td>" + outCall.outItem[0].detail.regione +  "</td></tr>"; ;

                htmlOut += "<tr><td>Istat Prov</td>";
                htmlOut += "<td>" + outCall.outItem[0].detail.istatProv + "</td></tr>"; ;

                htmlOut += "<tr><td>Istat Comune</td>";
                htmlOut += "<td>" + outCall.outItem[0].detail.istatComune + "</td></tr>"; ;

                htmlOut += "<tr><td>X</td>";
                htmlOut += "<td>" + outCall.outItem[0].geo.x + "</td></tr>"; ;

                htmlOut += "<tr><td>Y</td>";
                htmlOut += "<td>" + outCall.outItem[0].geo.y + "</td></tr>"; ;


                htmlOut += "</table>";
                outArea.InnerHtml = htmlOut;
            }
            else
            {
                // verifica KO, gestione errore

                // errore di licenza
                if (outCall.codErr == 997)
                    outArea.InnerHtml = "<p><font color=\"red\">LICENSE KEY NON RICONOSCIUTA</font></p>";
                else if (outCall.codErr == 123)
                    outArea.InnerHtml = "<p><font color=\"red\">NON E' STATO VALORIZZATO IL COMUNE</font></p>";
                else if (outCall.codErr == 124)
                    outArea.InnerHtml = "<p><font color=\"red\">COMUNE\\FRAZIONE NON RICONOSCIUTO</font></p>";
                else if (outCall.codErr == 125)
                {
                    String htmlOut= "<p><font color=\"red\">COMUNE\\FRAZIONE AMBIGUO</font></p>";

                    htmlOut += "<table>";
                    foreach (FillWS.outFill outElem in outCall.outItem)
                    {
                        htmlOut += "<tr><td>";

                        htmlOut += outElem.cap + " "+ outElem.comune+ " " + outElem.provincia;
                        if (outElem.frazione != string.Empty)
                            htmlOut += " - " + outElem.frazione;
                        htmlOut += "</td></tr>";
                    }
                    htmlOut += "</table>";
                    outArea.InnerHtml = htmlOut;
                }
                else if (outCall.codErr == 466)
                {
                    txtCap.Text = outCall.outItem[0].cap;
                    txtProv.Text = outCall.outItem[0].provincia;
                    txtComune.Text = outCall.outItem[0].comune;
                    txtFrazione.Text = outCall.outItem[0].frazione;
                    outArea.InnerHtml = "<p><font color=\"red\">NON E' STATO VALORIZZATO L'INDIRIZZO</font></p>";
                }
                else if (outCall.codErr == 467)
                {
                    txtCap.Text = outCall.outItem[0].cap;
                    txtProv.Text = outCall.outItem[0].provincia;
                    txtComune.Text = outCall.outItem[0].comune;
                    txtFrazione.Text = outCall.outItem[0].frazione;
                    outArea.InnerHtml = "<p><font color=\"red\">INDIRIZZO NON RICONOSCIUTO</font></p>";
                }
                else if (outCall.codErr == 468)
                {
                    txtCap.Text = outCall.outItem[0].cap;
                    txtProv.Text = outCall.outItem[0].provincia;
                    txtComune.Text = outCall.outItem[0].comune;
                
                    String htmlOut = "<p><font color=\"red\">INDIRIZZO AMBIGUO</font></p>";
                    htmlOut += "<table>";
                    foreach (FillWS.outFill outElem in outCall.outItem)
                    {
                        htmlOut += "<tr><td>";

                        htmlOut += outElem.cap + " " + outElem.indirizzo;
                        if (outElem.frazione != string.Empty)
                            htmlOut += " (" + outElem.frazione + ")";
                        htmlOut += "</td></tr>";
                    }
                    htmlOut += "</table>";
                    outArea.InnerHtml = htmlOut;
                }
                else if (outCall.codErr == 455)
                {
                    txtCap.Text = outCall.outItem[0].cap;
                    txtProv.Text = outCall.outItem[0].provincia;
                    txtComune.Text = outCall.outItem[0].comune;
                    txtFrazione.Text = outCall.outItem[0].frazione;
                    txtIndirizzo.Text = outCall.outItem[0].indirizzo;
                    outArea.InnerHtml = "<p><font color=\"red\">CAP INCONGRUENTE SU VIA MULTICAP</font></p>";
                }
            }
            outArea.Style["Border"] = "groove";
        }
    }
}