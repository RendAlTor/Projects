<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="ECP.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h3>&quot;Название организации&quot;</h3>
    <address>
        &quot;Адрес и контакты организации&quot;
    </address>

    <address>
        <strong>Support:</strong>   <a href="https://www.google.ru/">Support</a><br />
        <strong>Marketing:</strong> <a href="microsoft.com">Marketing</a>
    </address>
</asp:Content>
