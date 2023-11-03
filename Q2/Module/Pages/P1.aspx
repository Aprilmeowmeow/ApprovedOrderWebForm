<%@ Page Title="" Language="C#" MasterPageFile="~/layout/Layout_ExtNetTpl.master" AutoEventWireup="true" CodeFile="P1.aspx.cs" Inherits="Q2.Web.UI.P1" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ResourcePlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="StorePlaceHolder" runat="Server">
    <style type="text/css">
        .my-disabled .x-grid-row-checker {
            display: none;
            opacity: 0.6;
        }
    </style>
    <ext:Store ID="MainStore" runat="server" IDMode="Static">
        <Model>
            <ext:Model runat="server" IDProperty="ID">
                <Fields>
                    <ext:ModelField Name="ID" Type="String" />
                    <ext:ModelField Name="Customer_ID" Type="String" />
                    <ext:ModelField Name="TotalAmount" Type="Float" />
                    <ext:ModelField Name="Status" Type="Int" />
                    <ext:ModelField Name="Order_Date" Type="Date" />
                    <ext:ModelField Name="Sales_Name" Type="String" />
                    <ext:ModelField Name="Approved_Date" Type="Date" />
                    <ext:ModelField Name="Enable" Type="Int" />
                </Fields>
            </ext:Model>
        </Model>
    </ext:Store>
    <ext:Store ID="Store1" runat="server" IDMode="Static">
        <Model>
            <ext:Model runat="server" IDProperty="ID">
                <Fields>
                    <ext:ModelField Name="ID" Type="String" />
                    <ext:ModelField Name="Enable" Type="Int" />
                </Fields>
            </ext:Model>
        </Model>
    </ext:Store>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CenterPlaceHolder1" runat="Server">
    <ext:Window runat="server" ID="window1" Icon="Cog" Hidden="true"
        Collapsible="true" Constrain="true" MinHeight="400" MinWidth="500" Modal="true"
        CloseAction="Hide" BodyPaddingSummary="10" Layout="FitLayout">
        <Buttons>
            <ext:Button runat="server" ID="Button2" Text="Save">
                <DirectEvents>
                    <Click OnEvent="SaveCust"
                        Success="approvedSuccess"
                        Failure="approvedFailure">
                        <EventMask ShowMask="true" Target="Page" Msg="Store ing..." />
                        <ExtraParams>
                            <ext:Parameter Name="model" Mode="Raw"
                                Value="#{Grid1}.getRowsValues({selectedOnly:true})" />
                        </ExtraParams>
                    </Click>
                </DirectEvents>
            </ext:Button>
            <ext:Button runat="server" ID="btnMainCancel" Text="Cancel">
                <Listeners>
                    <Click Handler="#{window1}.close();" />
                </Listeners>
            </ext:Button>
        </Buttons>
        <Items>
            <ext:FormPanel
                ID="CustForm"
                runat="server"
                Icon="Book"
                Frame="true"
                LabelAlign="Right"
                Title="Customer -- All fields are required">
                <FieldDefaults LabelAlign="Right" AllowBlank="false" />
                <Items>
                    <ext:GridPanel
                        ID="Grid1"
                        runat="server"
                        Title="Company Data"
                        ColumnWidth="0.6"
                        Height="400"
                        StoreID="Store1">
                        <ColumnModel runat="server">
                            <Columns>
                                <ext:Column runat="server" Text="Customer" Width="75" DataIndex="ID">
                                </ext:Column>
                                <ext:Column runat="server" Text="Enable" Width="75" DataIndex="Enable">
                                    <Editor>
                                        <ext:NumberField runat="server" MaxValue="1" MinValue="0" />
                                    </Editor>
                                </ext:Column>
                            </Columns>
                        </ColumnModel>
                        <SelectionModel>
                            <ext:RowSelectionModel runat="server" />
                        </SelectionModel>
                        <Plugins>
                            <ext:CellEditing runat="server" ClicksToEdit="1" />
                        </Plugins>

                    </ext:GridPanel>
                </Items>
            </ext:FormPanel>
        </Items>
    </ext:Window>
    <ext:Panel ID="Panel2" Title="Approve Order" runat="server" BodyBorder="0" Layout="FitLayout">
        <Items>
            <ext:GridPanel ID="MainGrid" runat="server" StoreID="MainStore" MultiSelect="true">
                <DockedItems>
                        <ext:Toolbar runat="server" Dock="Top">
                            <Items>
                                <ext:Button runat="server" ID="Button3" Icon="Cog" ToolTip="Set Enable">
                                    <Listeners>
                                        <Click Fn="setCustEnable" />
                                    </Listeners>
                                </ext:Button>
                            </Items>
                        </ext:Toolbar>
                    </DockedItems>  
                <ColumnModel>
                    <Columns>
                        <ext:CommandColumn runat="server" ID="approved" Width="30" Locked="true" Sortable="false">
                            <PrepareToolbar Fn="prepareToolbar" />
                            <Commands>
                                <ext:GridCommand Icon="BookGo" CommandName="Approve" ToolTip-Text="Click to approve this order" />
                            </Commands>
                            <Listeners>
                                <Command Fn="onCommand" />
                            </Listeners>
                        </ext:CommandColumn>
                        <ext:Column runat="server" ID="colID" Text="Order#" DataIndex="ID" Width="80" Locked="true" />
                        <ext:Column runat="server" ID="colCustomerID" Text="Customer" DataIndex="Customer_ID" Width="120" Locked="true" />
                        <ext:Column runat="server" ID="colStatus" Text="Status" DataIndex="Status" Width="80" Locked="true">
                            <Renderer Fn="onStatus_Renderer" />
                        </ext:Column>
                        <ext:NumberColumn runat="server" ID="colAmount" Text="Amount" DataIndex="TotalAmount" Width="100" Lockable="false" Format="$0,000.00" Align="Right" />
                        <ext:DateColumn runat="server" ID="colODate" Text="Date" DataIndex="Order_Date" Width="100" Lockable="false" Format="Y-m-dd" Align="Center" />
                        <ext:Column runat="server" ID="colSalesName" Text="Sales" DataIndex="Sales_Name" Width="80" Lockable="false" />
                        <ext:DateColumn runat="server" ID="colAprDate" Text="ApprovedDate" DataIndex="Approved_Date" Width="100" Lockable="false" Format="Y-m-dd" Align="Center" />
                        <ext:Column runat="server" ID="colEnable" Text="Enable" DataIndex="Enable" Width="80" Locked="true">
                            <Renderer Fn="onEnable_Renderer" />
                        </ext:Column>
                    </Columns>
                </ColumnModel>
                <BottomBar>
                    <ext:PagingToolbar runat="server" DisplayInfo="false" HideRefresh="true">
                        <Items>
                            <ext:Button ID="Button1" runat="server" Text="Submit Selected Records" StandOut="true">
                                <DirectEvents>
                                    <Click OnEvent="Button1_Click"
                                        Success="approvedSuccess"
                                        Failure="approvedFailure">
                                        <EventMask ShowMask="true" />
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                        </Items>
                    </ext:PagingToolbar>
                </BottomBar>
                <SelectionModel>
                    <ext:CheckboxSelectionModel runat="server" Mode="Multi" InjectCheckbox="0" CheckOnly="true">
                        <Renderer Fn="hide" />
                    </ext:CheckboxSelectionModel>
                </SelectionModel>
            </ext:GridPanel>

        </Items>
    </ext:Panel>

</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FooterPlaceHolder1" runat="Server">
    <script>
       
        var setCustEnable = function (sender, e) {
            window1.setTitle('Set Customer');
            window1.show(sender);
        };

        var prepareToolbar = function (grid, toolbar, rowIndex, record) {

            if (record.data.Status == 5 || record.data.Enable === 0) {
                toolbar.items.items[0].hide();
            }
        };
        var hide = function (value, metaData, record, rowIndex, colIndex, store, view) {
            metaData.tdCls = Ext.baseCSSPrefix + 'grid-cell-special';
            var cbClass = Ext.baseCSSPrefix + 'grid-row-checker';
            var retval = '<div class="' + cbClass + '"';
            if (record.data.Status === 5 || record.data.Enable === 0) {
                return "";
            }
            retval = retval + '></div>';
            return retval;
        }
        var onCommand = function (command, commandName, record, rowIndex) {
            if (record.data.Status == 5) {
                MainGrid.columnManager.headerCt.getGridColumns()[1].hide();
            }
            Ext.MessageBox.confirm("Tip", "Are you sure?", callBackFunc);
            function callBackFunc(id) {
                if (id == 'yes') {
                    switch (commandName) {
                        case 'Approve': setApprove(record); break;
                    }
                }
            }

        };
        
        
        var setApprove = function (record) {
            Ext.net.DirectMethods.ExecuteApprove(record.get("ID"), {
                success: function (result) {
                    //Ext.msg.alert(result);
                    MainGrid.getStore().reload();
                    Ext.Msg.alert("tip", "Save Success");
                },
                failure: function (errorMsg) {
                    Ext.Msg.alert('Failure', errorMsg);
                }
            })
        };

        var approvedSuccess = function (result) {
            Ext.Msg.notify("tip", "Save Success");
        }

        var approvedFailure = function(response, result,
            control, type, action, extraParams) {
            Ext.Msg.alert("Failure", result.errorMessage);
        }

        var onStatus_Renderer = function (value, meta, record, rowindex, colindex) {
            var v = value;

            switch (v) {
                case 0: v = "Void"; break;
                case 1: v = "Draft"; break;
                case 2: v = "Open"; break;
                case 3: v = "Completed"; break;
                case 4: v = "Invoiced"; break;
                case 5: v = "Approved"; break;
            }

            return v;
        };
        var onEnable_Renderer = function (value, meta, record, rowindex, colindex) {
            var v = value;

            switch (v) {
                case 0: v = "Unable"; break;
                case 1: v = "Enable"; break;
            }
            return v;
        };
    </script>
</asp:Content>

