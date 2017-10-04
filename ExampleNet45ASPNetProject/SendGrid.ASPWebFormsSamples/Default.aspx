<%@ Page Title="Home Page" Async="true" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SendGrid.ASPWebFormsSamples.Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-4">
            <div class="form-horizontal">
                <div class="form-group">
                    <label class="col-md-2 control-label">From: </label>
                    <div class="col-md-10">
                        <input id="fromInput" runat="server" type="text" class="form-control" />
                    </div>
                </div>
    
                <div class="form-group">
                    <label class="col-md-2 control-label">To: </label>
                    <div class="col-md-10">
                        <input id="toInput" runat="server" type="text" class="form-control"/>
                    </div>
                </div>
    
                <div class="form-group">
                    <label class="col-md-2 control-label">Cc: </label>
                    <div class="col-md-10">
                        <input id="ccInput" runat="server" type="text" class="form-control"/>
                    </div>
                </div>
            
                <div class="form-group">
                    <label class="col-md-2 control-label">Bcc: </label>
                    <div class="col-md-10">
                        <input id="bccInput" runat="server" type="text" class="form-control"/>
                    </div>
                </div>
    
                <div class="form-group">
                    <label class="col-md-2 control-label">Subject: </label>
                    <div class="col-md-10">
                        <input id="subjectInput" runat="server" type="text" class="form-control"/>
                    </div>
                </div>
    
                <div class="form-group">
                    <label class="col-md-2 control-label">Body: </label>
                    <div class="col-md-10">
                        <textarea id="bodyInput" runat="server" cols="20" rows="10" class="form-control"></textarea>
                    </div>
                </div>

                <div class="form-group">
                    <div class="col-sm-offset-2 col-sm-10">
                        <asp:Button ID="sendButton" runat="server" Text="Send" OnClick="sendButton_Click" CssClass="btn btn-default pull-right"/>
                    </div>
                </div>
            </div>
        </div>
        
        <div id="responseInfo" runat="server" class="col-md-8">
            <div id="responseStatus" runat="server" class="alert alert-info" role="alert"></div>

            <span>Response body: </span>
            <div runat="server" id="responseBody" class="well"></div>
        </div>
    </div>
</asp:Content>