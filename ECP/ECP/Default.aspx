<%@ Page Title="Easy.Safe" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ECP._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Название сайта, краткое и запоминающееся</h1>
        <p class="lead">Easy.Safe это Ваша бесплатная возможность обмениваться данными - Безопасно</p>
        <p><a href="http://localhost:46485/UploadPage1/UploadForm1" class="btn btn-primary btn-lg">Попробуй прямо сейчас &raquo;</a></p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Зарегистрируйся!</h2>
            <p>
                Чтобы получить доступ к возможностям Easy.Safe, достаточно пройти простую регистрацию. 
            </p>
            <p>
                <a class="btn btn-default" href="http://localhost:46485/Account/Register">Форма регистрации &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Загрузка</h2>
            <p>
                Здесь Вы сможете разместить, любые файлы, на Ваше усмотрение.
            </p>
            <p>
                <a class="btn btn-default" href="http://localhost:46485/UploadPage1/UploadForm1">Загрузить! &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Скачивание</h2>
            <p>
                Вы можете просмотреть доступный контент и получить его.
            </p>
            <p>
                <a class="btn btn-default" href="http://localhost:46485/UploadPage1/UploadForm1">Скачать! &raquo;</a>
            </p>
        </div>
    </div>
</asp:Content>
