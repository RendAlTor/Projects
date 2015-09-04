<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="UploadForm1.aspx.cs" Inherits="ECP.UploadPage1.UploadForm1" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="font-family:Arial">
    
        <asp:FileUpload ID="FileUpload1" runat="server" />
        <asp:Button ID="Button1" runat="server" Text="Upload" OnClick="Button1_Click" />
        <br />
        <br />
    
        <asp:TextBox ID="TextBox1" runat="server">Подпись</asp:TextBox>
        <asp:TextBox ID="TextBox2" runat="server">Открытый ключ</asp:TextBox>
        <asp:TextBox ID="TextBox4" runat="server">Модуль</asp:TextBox>
        <asp:Button ID="Button2" runat="server" Height="21px" OnClick="Button2_Click" Text="Проверить" Width="86px" />
        <asp:TextBox ID="TextBox3" runat="server">Хэш</asp:TextBox>
    
    </div>
        <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" >
            <Columns>
                <asp:TemplateField HeaderText="Файл">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("File") %>' CommandName="Download" Text='<%# Eval("File") %>'></asp:LinkButton> 
                    </ItemTemplate>
                    <ItemStyle Width="10%" />
                </asp:TemplateField>
                <asp:BoundField DataField="Size" HeaderText="Размер (bytes)" >
                <ItemStyle Width="200px" />
                </asp:BoundField>
                <asp:BoundField DataField="Type" HeaderText="Тип файла" />
                <asp:BoundField DataField="Hash" HeaderText="Хэш">
                <ItemStyle Width="10%" />
                </asp:BoundField>    
                <asp:BoundField DataField="Sign" HeaderText="Подпись">
                <ItemStyle Width="10%" Wrap="True"/>                
                </asp:BoundField>
                <asp:BoundField DataField="PublicKey" HeaderText="Ключ" ItemStyle-Width="20px">
                <ItemStyle Width="200px" Wrap="True"/>
                </asp:BoundField>
                <asp:BoundField DataField="Mod" HeaderText="Модуль">
                <ItemStyle Width="10%" Wrap="True"/>
                </asp:BoundField>    
                <asp:BoundField DataField="Login" HeaderText="Login" />
            </Columns>
            <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
            <RowStyle BackColor="White" ForeColor="#330099" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
            <SortedAscendingCellStyle BackColor="#FEFCEB" />
            <SortedAscendingHeaderStyle BackColor="#AF0101" />
            <SortedDescendingCellStyle BackColor="#F6F0C0" />
            <SortedDescendingHeaderStyle BackColor="#7E0000" />
        </asp:GridView>
    </asp:Content>
