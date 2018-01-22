<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MLASPWebPage.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function dataValid() {
            //Validate age
            var inAge = document.getElementById("txtAge").value;
            if ((inAge < 0) || (inAge > 110)) {
                alert("Please an age between 0 - 110");
                return false;
            }

            //Validate fnlwgt
            var inFnlWgt = document.getElementById("txtFnlWgt").value;
            if ((inFnlWgt < 12285) || (inAge > 1484705)) {
                alert("Please an FnlWgt between 12285 - 1484705");
                return false;
            }

            //Validate EducationNum
            var inEducationNum = document.getElementById("txtEducationNum").value;
            if ((inEducationNum < 1) || (inEducationNum > 16)) {
                alert("Please a Education between 1 - 16");
                return false;
            }

            //Validate capitalgain
            var inCapitalGain = document.getElementById("txtCapitalGain").value;
            if ((inCapitalGain < 0) || (inCapitalGain > 99999)) {
                alert("Please a CapitalGain between 0 - 99999");
                return false;
            }


            //Validate capitalloss
            var inCapitalLoss = document.getElementById("txtCapitalLoss").value;
            if ((inCapitalLoss < 0) || (inCapitalLoss > 4356)) {
                alert("Please a CapitalLoss between 0 - 4356");
                return false;
            }

            //Validate hoursperweek
            var inHoursPerWeek = document.getElementById("txtHoursPerWeek").value;
            if ((inHoursPerWeek < 1) || (inHoursPerWeek > 99)) {
                alert("Please a Hours Per Week between 1 - 99");
                return false;
            }


        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="float: left;">
            <h3>Azure ML Predictive Model Tester - Income Prediction</h3>
            <table>
                <tr>
                    <td>Age: </td>
                    <td>
                        <asp:TextBox ID="txtAge" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Work Class: </td>
                    <td>
                        <asp:DropDownList ID="ddlWorkClass" runat="server">
                            <asp:ListItem Text="Private" Value="Private"></asp:ListItem>
                            <asp:ListItem Text="Self-emp-not-inc" Value="Self-emp-not-inc"></asp:ListItem>
                            <asp:ListItem Text="Self-emp-inc" Value="Self-emp-inc"></asp:ListItem>
                            <asp:ListItem Text="Federal-gov" Value="Federal-gov"></asp:ListItem>
                            <asp:ListItem Text="Local-gov" Value="Local-gov"></asp:ListItem>
                            <asp:ListItem Text="State-gov" Value="State-gov"></asp:ListItem>
                            <asp:ListItem Text="Without-pay" Value="Without-pay"></asp:ListItem>
                            <asp:ListItem Text="Never-worked" Value="Never-worked"></asp:ListItem>
                        </asp:DropDownList>

                    </td>
                </tr>

                <tr>
                    <td>Fnlwgt: </td>
                    <td>
                        <asp:TextBox ID="txtFnlWgt" runat="server"></asp:TextBox></td>
                </tr>


                <tr>
                    <td>
                        <%--refereces: https://archive.ics.uci.edu/ml/datasets/census+income--%>
                        <label>Educations: </label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlEducation" CssClass="lbox" runat="server" AppendDataBoundItems="true" DataTextField="Company_Name" DataValueField="id">
                            <asp:ListItem Text="Bachelors" Value="Bachelors"></asp:ListItem>
                            <asp:ListItem Text="Some-college" Value="Some-college"></asp:ListItem>
                            <asp:ListItem Text="11th" Value="11th"></asp:ListItem>
                            <asp:ListItem Text="HS-grad" Value="HS-grad"></asp:ListItem>
                            <asp:ListItem Text="Prof-school" Value="Prof-school"></asp:ListItem>
                            <asp:ListItem Text="Assoc-acdm" Value="Assoc-acdm"></asp:ListItem>
                            <asp:ListItem Text="Assoc-voc" Value="Assoc-voc"></asp:ListItem>
                            <asp:ListItem Text="9th" Value="9th"></asp:ListItem>
                            <asp:ListItem Text="7th-8th" Value="7th-8th"></asp:ListItem>
                            <asp:ListItem Text="12th" Value="12th"></asp:ListItem>
                            <asp:ListItem Text="Masters" Value="Masters"></asp:ListItem>
                            <asp:ListItem Text="1st-4th" Value="1st-4th"></asp:ListItem>
                            <asp:ListItem Text="10th" Value="10th"></asp:ListItem>
                            <asp:ListItem Text="Doctorate" Value="Doctorate"></asp:ListItem>
                            <asp:ListItem Text="5th-6th" Value="5th-6th"></asp:ListItem>
                            <asp:ListItem Text="Preschool" Value="Preschool"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>

                <tr>
                    <td>Education-num: </td>
                    <td>
                        <asp:TextBox ID="txtEducationNum" runat="server"></asp:TextBox></td>
                </tr>

                <tr>
                    <td>
                        <label>Marital-Status: </label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlMaritalStatus" runat="server">
                            <asp:ListItem Text="Married-civ-spouse" Value="Married-civ-spouse"></asp:ListItem>
                            <asp:ListItem Text="Divorced" Value="Divorced"></asp:ListItem>
                            <asp:ListItem Text="Never-married" Value="Never-married"></asp:ListItem>
                            <asp:ListItem Text="Separated" Value="Separated"></asp:ListItem>
                            <asp:ListItem Text="Widowed" Value="Widowed"></asp:ListItem>
                            <asp:ListItem Text="Married-spouse-absent" Value="Married-spouse-absent"></asp:ListItem>
                            <asp:ListItem Text="Married-AF-spouse" Value="Married-AF-spouse"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>

                <tr>
                    <td>
                        <label>Occupation: </label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlOccupation" runat="server">
                            <asp:ListItem Text="Tech-support" Value="Tech-support"></asp:ListItem>
                            <asp:ListItem Text="Craft-repair" Value="Craft-repair"></asp:ListItem>
                            <asp:ListItem Text="Other-service" Value="Other-service"></asp:ListItem>
                            <asp:ListItem Text="Sales" Value="Sales"></asp:ListItem>
                            <asp:ListItem Text="Exec-managerial" Value="Exec-managerial"></asp:ListItem>
                            <asp:ListItem Text="Prof-specialty" Value="Prof-specialty"></asp:ListItem>
                            <asp:ListItem Text="Handlers-cleaners" Value="Handlers-cleaners"></asp:ListItem>
                            <asp:ListItem Text="Machine-op-inspct" Value="Machine-op-inspct"></asp:ListItem>
                            <asp:ListItem Text="Adm-clerical" Value="Adm-clerical"></asp:ListItem>
                            <asp:ListItem Text="Farming-fishing" Value="Farming-fishing"></asp:ListItem>
                            <asp:ListItem Text="Transport-moving" Value="Transport-moving"></asp:ListItem>
                            <asp:ListItem Text="Priv-house-serv" Value="Priv-house-serv"></asp:ListItem>
                            <asp:ListItem Text="Protective-serv" Value="Protective-serv"></asp:ListItem>
                            <asp:ListItem Text="Armed-Forces" Value="Armed-Forces"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>

                <tr>
                    <td>
                        <label>Relationship: </label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlRelationship" runat="server">
                            <asp:ListItem Text="Wife" Value="Wife"></asp:ListItem>
                            <asp:ListItem Text="Own-child" Value="Own-child"></asp:ListItem>
                            <asp:ListItem Text="Husband" Value="Husband"></asp:ListItem>
                            <asp:ListItem Text="Not-in-family" Value="Not-in-family"></asp:ListItem>
                            <asp:ListItem Text="Other-relative" Value="Other-relative"></asp:ListItem>
                            <asp:ListItem Text="Unmarried" Value="Unmarried"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>


                <tr>
                    <td>
                        <label>Race: </label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlRace" runat="server">
                            <asp:ListItem Text="White" Value="White"></asp:ListItem>
                            <asp:ListItem Text="Asian-Pac-Islander" Value="Asian-Pac-Islander"></asp:ListItem>
                            <asp:ListItem Text="Amer-Indian-Eskimo" Value="Amer-Indian-Eskimo"></asp:ListItem>
                            <asp:ListItem Text="Other" Value="Other"></asp:ListItem>
                            <asp:ListItem Text="Black" Value="Black"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>


                <tr>
                    <td>
                        <label>Sex: </label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSex" runat="server">
                            <asp:ListItem Text="Female" Value="Female"></asp:ListItem>
                            <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>


                <tr>
                    <td>Capital-gain: </td>
                    <td>
                        <asp:TextBox ID="txtCapitalGain" runat="server"></asp:TextBox></td>
                </tr>


                <tr>
                    <td>Capital-loss: </td>
                    <td>
                        <asp:TextBox ID="txtCapitalLoss" runat="server"></asp:TextBox></td>
                </tr>


                <tr>
                    <td>Hours-per-week: </td>
                    <td>
                        <asp:TextBox ID="txtHoursPerWeek" runat="server"></asp:TextBox></td>
                </tr>


                <tr>
                    <td>
                        <label>Native-Country: </label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlNativeCountry" runat="server">
                            <asp:ListItem Text="United-States" Value="United-States"></asp:ListItem>
                            <asp:ListItem Text="Cambodia" Value="Cambodia"></asp:ListItem>
                            <asp:ListItem Text="England" Value="England"></asp:ListItem>
                            <asp:ListItem Text="Puerto-Rico" Value="Puerto-Rico"></asp:ListItem>
                            <asp:ListItem Text="Canada" Value="Canada"></asp:ListItem>
                            <asp:ListItem Text="Germany" Value="Germany"></asp:ListItem>
                            <asp:ListItem Text="Outlying-US(Guam-USVI-etc)" Value="Outlying-US(Guam-USVI-etc)"></asp:ListItem>
                            <asp:ListItem Text="India" Value="India"></asp:ListItem>
                            <asp:ListItem Text="Japan" Value="Japan"></asp:ListItem>
                            <asp:ListItem Text="Greece" Value="Greece"></asp:ListItem>
                            <asp:ListItem Text="South" Value="South"></asp:ListItem>
                            <asp:ListItem Text="China" Value="China"></asp:ListItem>
                            <asp:ListItem Text="Cuba" Value="Cuba"></asp:ListItem>
                            <asp:ListItem Text="Iran" Value="Iran"></asp:ListItem>

                            <asp:ListItem Text="Honduras" Value="Honduras"></asp:ListItem>
                            <asp:ListItem Text="Philippines" Value="Philippines"></asp:ListItem>
                            <asp:ListItem Text="Italy" Value="Italy"></asp:ListItem>
                            <asp:ListItem Text="Poland" Value="Poland"></asp:ListItem>
                            <asp:ListItem Text="Jamaica" Value="Jamaica"></asp:ListItem>
                            <asp:ListItem Text="Vietnam" Value="Vietnam"></asp:ListItem>
                            <asp:ListItem Text="Mexico" Value="Mexico"></asp:ListItem>
                            <asp:ListItem Text="Portugal" Value="Portugal"></asp:ListItem>
                            <asp:ListItem Text="Ireland" Value="Ireland"></asp:ListItem>
                            <asp:ListItem Text="France" Value="France"></asp:ListItem>
                            <asp:ListItem Text="Dominican-Republic" Value=" Dominican-Republic"></asp:ListItem>
                            <asp:ListItem Text="Laos" Value="Laos"></asp:ListItem>
                            <asp:ListItem Text="Ecuador" Value="Ecuador"></asp:ListItem>
                            <asp:ListItem Text="Taiwan" Value="Taiwan"></asp:ListItem>


                            <asp:ListItem Text="Haiti" Value="Haiti"></asp:ListItem>
                            <asp:ListItem Text="Columbia" Value="Columbia"></asp:ListItem>
                            <asp:ListItem Text="Hungary" Value="Hungary"></asp:ListItem>
                            <asp:ListItem Text="Guatemala" Value="Guatemala"></asp:ListItem>
                            <asp:ListItem Text="Nicaragua" Value="Nicaragua"></asp:ListItem>
                            <asp:ListItem Text="Scotland" Value="Scotland"></asp:ListItem>
                            <asp:ListItem Text="Thailand" Value="Thailand"></asp:ListItem>
                            <asp:ListItem Text="Yugoslavia" Value="Yugoslavia"></asp:ListItem>
                            <asp:ListItem Text="El-Salvador" Value="El-Salvador"></asp:ListItem>
                            <asp:ListItem Text="Trinadad&Tobago" Value="Trinadad&Tobago"></asp:ListItem>
                            <asp:ListItem Text="Peru" Value="Peru"></asp:ListItem>
                            <asp:ListItem Text="Hong" Value="Hong"></asp:ListItem>
                            <asp:ListItem Text=" Holand-Netherlands" Value=" Holand-Netherlands"></asp:ListItem>

                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <asp:Button ID="btnPrediction" runat="server" Text="Make Prediction!" OnClick="btnPrediction_Click" OnClientClick="return dataValid();" />
        </div>


        <div style="float: left; background-color: azure; min-height: 600px;" runat="server" id="divPanel">
            <asp:BulletedList ID="BulletedList1" runat="server"
                BulletStyle="Circle"
                DisplayMode="Text">
            </asp:BulletedList>
        </div>
    </form>
</body>
</html>
