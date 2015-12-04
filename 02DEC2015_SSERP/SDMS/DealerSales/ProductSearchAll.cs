﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
using SSAdmin;
using SSTrans;
namespace SDMS
{
    public partial class ProductSearchAll : Form
    {
        private SQLDB objSQLDb;
        private InvoiceDB objInv = null;
        public InvoiceTemplateProducts objInvoiceTemplateProducts;
        private StockTransferTrn objStockTransferTrn;
        public OrderBookingForm objFrmOrderBooking;
        public DealerInvoice objDealerInvoice;        
        private string strInvType = string.Empty;
        private int iECODE = 0;
        public ProductSearchAll(string sInvType)
        {
            InitializeComponent();
            strInvType = sInvType;
        }
        public ProductSearchAll(string sInvType, int eCode)
        {
            InitializeComponent();
            strInvType = sInvType;
            iECODE = eCode;
        }
        private void ProductSearch_Load(object sender, EventArgs e)
        {
            if (strInvType == "FreeProduct")
                FillFreeProducts();
            else if ((strInvType == "OpeningStock") || (strInvType == "INTERNALDAMAGE") || (strInvType == "SPRefill_Sours") || (strInvType == "SPRefill_dist") || (strInvType == "OrderBooking"))
                FillSingleProducts();
            else if (strInvType == "StockTransfer")
                FillStockTransferProducts(iECODE);
            else if (strInvType == "ProductPriceCircular")
                FillSingleAndCombiProducts();
            else if(CommonData.CompanyCode=="SATL")
            {
                FillDealerProducts();
            }
            else
                FillProducts();
        }
        private void FillSingleAndCombiProducts()
        {

            objInv = new InvoiceDB();
            DataSet ds = new DataSet();
            ds = objInv.InvProductSearchCursor_Get(CommonData.CompanyCode);
            TreeNode tNode;
            tNode = tvProducts.Nodes.Add("Products");
            tvProducts.Nodes[0].Nodes.Add("Single Product");
            try
            {
                if (ds.Tables[0].Rows.Count > 0)
                {


                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        //int iCatNod = 0;
                        //if (tvProducts.Nodes[0].Nodes[0].Nodes.Count == 0)
                        //{
                        //    tvProducts.Nodes[0].Nodes[0].Nodes.Add(ds.Tables[0].Rows[i]["CategoryName"].ToString(),
                        //                                                        ds.Tables[0].Rows[i]["CategoryName"].ToString());
                        //    tvProducts.Nodes[0].Nodes[0].Nodes[iCatNod].Nodes.Add(ds.Tables[0].Rows[i]["ProductID"] + "^".ToString() +
                        //                                       ds.Tables[0].Rows[i]["ProductName"] + "^".ToString() +
                        //                                       ds.Tables[0].Rows[i]["SingleMRP"] + "^".ToString() +
                        //                                       ds.Tables[0].Rows[i]["BulkMRP"] + "^".ToString() +
                        //                                       ds.Tables[0].Rows[i]["CategoryName"] + "^".ToString() +
                        //                                       ds.Tables[0].Rows[i]["Points"] + "^".ToString() +
                        //                                       ds.Tables[0].Rows[i]["CategoryID"] + "^".ToString() +
                        //                                       ds.Tables[0].Rows[i]["ProductType"] + "".ToString(), ds.Tables[0].Rows[i]["ProductName"] + "(Rs = ".ToString() + ds.Tables[0].Rows[i]["SingleMRP"] + ")".ToString());
                        //}
                        //else
                        //{
                        //    if (ds.Tables[0].Rows[i]["CategoryName"].ToString() == ds.Tables[0].Rows[i-1]["CategoryName"].ToString())
                        //    {
                        //        tvProducts.Nodes[0].Nodes[0].Nodes[iCatNod].Nodes.Add(ds.Tables[0].Rows[i]["ProductID"] + "^".ToString() +
                        //                                       ds.Tables[0].Rows[i]["ProductName"] + "^".ToString() +
                        //                                       ds.Tables[0].Rows[i]["SingleMRP"] + "^".ToString() +
                        //                                       ds.Tables[0].Rows[i]["BulkMRP"] + "^".ToString() +
                        //                                       ds.Tables[0].Rows[i]["CategoryName"] + "^".ToString() +
                        //                                       ds.Tables[0].Rows[i]["Points"] + "^".ToString() +
                        //                                       ds.Tables[0].Rows[i]["CategoryID"] + "^".ToString() +
                        //                                       ds.Tables[0].Rows[i]["ProductType"] + "".ToString(), ds.Tables[0].Rows[i]["ProductName"] + "(Rs = ".ToString() + ds.Tables[0].Rows[i]["SingleMRP"] + ")".ToString());
                        //    }
                        //}
                        
                        tvProducts.Nodes[0].Nodes[0].Nodes.Add(ds.Tables[0].Rows[i]["ProductID"] + "^".ToString() +
                                                               ds.Tables[0].Rows[i]["ProductName"] + "^".ToString() +
                                                               ds.Tables[0].Rows[i]["SingleMRP"] + "^".ToString() +
                                                               ds.Tables[0].Rows[i]["BulkMRP"] + "^".ToString() +
                                                               ds.Tables[0].Rows[i]["CategoryName"] + "^".ToString() +
                                                               ds.Tables[0].Rows[i]["Points"] + "^".ToString() +
                                                               ds.Tables[0].Rows[i]["CategoryID"] + "^".ToString() +
                                                               ds.Tables[0].Rows[i]["ProductType"] + "".ToString(), ds.Tables[0].Rows[i]["ProductName"] + "(Rs = ".ToString() + ds.Tables[0].Rows[i]["SingleMRP"] + ")".ToString());
                    }


                }

                tvProducts.Nodes[0].Nodes.Add("Combi Pack");
                if (ds.Tables[2].Rows.Count > 0)
                {


                    for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                    {
                        tvProducts.Nodes[0].Nodes[1].Nodes.Add(ds.Tables[2].Rows[i]["ProductID"] + "^".ToString() +
                                                               ds.Tables[2].Rows[i]["ProductName"] + "^".ToString() +
                                                               ds.Tables[2].Rows[i]["SingleMRP"] + "^".ToString() +
                                                               ds.Tables[2].Rows[i]["BulkMRP"] + "^".ToString() +
                                                               ds.Tables[2].Rows[i]["CategoryName"] + "^".ToString() +
                                                               ds.Tables[2].Rows[i]["Points"] + "^".ToString() +
                                                               ds.Tables[2].Rows[i]["CategoryID"] + "^".ToString() +
                                                               ds.Tables[2].Rows[i]["ProductType"] + "".ToString(), ds.Tables[2].Rows[i]["ProductName"] + "(Rs = ".ToString() + Convert.ToDouble(ds.Tables[2].Rows[i]["BulkMRP"]).ToString("f") + ")".ToString());

                    }


                }
            }
            catch (Exception ex)
            {
                objInv = null;
                ds = null;

            }
            this.tvProducts.SelectedNode = tNode;
            this.tvProducts.SelectedNode.Expand();

        }

        private void FillStockTransferProducts(int iECODE)
        {
            try
            {
                objStockTransferTrn = new StockTransferTrn();
                DataSet ds = new DataSet();
                ds = objStockTransferTrn.GetStockTrnsfer(iECODE);
                TreeNode tNode;
                tNode = tvProducts.Nodes.Add("Products");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    tvProducts.Nodes[0].Nodes.Add("Product");
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        tvProducts.Nodes[0].Nodes[0].Nodes.Add(ds.Tables[0].Rows[i]["glstk_product_id"] + "^".ToString() +
                                                               ds.Tables[0].Rows[i]["glstk_product_name"] + "^".ToString() + ds.Tables[0].Rows[i]["glstk_cl_qty"] + "".ToString(), ds.Tables[0].Rows[i]["glstk_product_name"] + "(Qty = ".ToString() + ds.Tables[0].Rows[i]["glstk_cl_qty"] + ")".ToString());
                    }
                }
                this.tvProducts.SelectedNode = tNode.Nodes[0];
                this.tvProducts.SelectedNode.Expand();
            }
            catch (Exception ex)
            {
            }
        }

        private void FillSingleProducts()
        {
            objInv = new InvoiceDB();
            DataSet ds = new DataSet();
            ds = objInv.InvProductSearchCursor_Get(CommonData.CompanyCode);
            TreeNode tNode;
            tNode = tvProducts.Nodes.Add("Products");
            tvProducts.Nodes[0].Nodes.Add("Single Product");
            if (ds.Tables[0].Rows.Count > 0)
            {


                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    tvProducts.Nodes[0].Nodes[0].Nodes.Add(ds.Tables[0].Rows[i]["ProductID"] + "^".ToString() +
                                                           ds.Tables[0].Rows[i]["SingleMRP"] + "^".ToString() +
                                                           ds.Tables[0].Rows[i]["BulkMRP"] + "^".ToString() +
                                                           ds.Tables[0].Rows[i]["CategoryName"] + "^".ToString() +
                                                           ds.Tables[0].Rows[i]["Points"] + "^".ToString() +
                                                           ds.Tables[0].Rows[i]["CategoryID"] + "^".ToString() +
                                                           ds.Tables[0].Rows[i]["ProductType"] + "^".ToString() +
                                                           ds.Tables[0].Rows[i]["UNIT_WEIGHT"] + "^".ToString() +
                                                           ds.Tables[0].Rows[i]["WEIGHT_TYPE"] + "".ToString(), ds.Tables[0].Rows[i]["ProductName"] + "(Rs = ".ToString() + ds.Tables[0].Rows[i]["SingleMRP"] + ")".ToString());
                }


            }
            this.tvProducts.SelectedNode = tNode.Nodes[0];
            this.tvProducts.SelectedNode.Expand();
        }
        private void FillProducts()
        {
            objInv = new InvoiceDB();
            DataSet ds = new DataSet();
            ds = objInv.InvProductSearchCursor_Get(CommonData.CompanyCode);
            TreeNode tNode;
            tNode = tvProducts.Nodes.Add("Products");
            tvProducts.Nodes[0].Nodes.Add("Single Product");
            if (ds.Tables[0].Rows.Count > 0)
            {


                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    tvProducts.Nodes[0].Nodes[0].Nodes.Add(ds.Tables[0].Rows[i]["ProductID"] + "^".ToString() +
                                                           ds.Tables[0].Rows[i]["SingleMRP"] + "^".ToString() +
                                                           ds.Tables[0].Rows[i]["BulkMRP"] + "^".ToString() +
                                                           ds.Tables[0].Rows[i]["CategoryName"] + "^".ToString() +
                                                           ds.Tables[0].Rows[i]["Points"] + "^".ToString() +
                                                           ds.Tables[0].Rows[i]["CategoryID"] + "^".ToString() +
                                                           ds.Tables[0].Rows[i]["ProductType"] + "".ToString(), ds.Tables[0].Rows[i]["ProductName"] + "(Rs = ".ToString() + ds.Tables[0].Rows[i]["SingleMRP"] + ")".ToString());
                }


            }
            tvProducts.Nodes[0].Nodes.Add("Invoice Product");
            if (ds.Tables[1].Rows.Count > 0)
            {


                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    tvProducts.Nodes[0].Nodes[1].Nodes.Add(ds.Tables[1].Rows[i]["ProductID"] + "^".ToString() +
                                                           ds.Tables[1].Rows[i]["SingleMRP"] + "^".ToString() +
                                                           ds.Tables[1].Rows[i]["BulkMRP"] + "^".ToString() +
                                                           ds.Tables[1].Rows[i]["CategoryName"] + "^".ToString() +
                                                           ds.Tables[1].Rows[i]["Points"] + "^".ToString() +
                                                           ds.Tables[1].Rows[i]["CategoryID"] + "^".ToString() +
                                                           ds.Tables[1].Rows[i]["ProductType"] + "".ToString(), ds.Tables[1].Rows[i]["ProductName"] + " (Rs = ".ToString() + Convert.ToDouble(ds.Tables[1].Rows[i]["SingleMRP"]).ToString("f") + ", ".ToString() + "P = " + Convert.ToDouble(ds.Tables[1].Rows[i]["Points"]).ToString("f") + ")");
                }


            }
            tvProducts.Nodes[0].Nodes.Add("Combi Pack");
            if (ds.Tables[2].Rows.Count > 0)
            {


                for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                {
                    tvProducts.Nodes[0].Nodes[2].Nodes.Add(ds.Tables[2].Rows[i]["ProductID"] + "^".ToString() +
                                                           ds.Tables[2].Rows[i]["SingleMRP"] + "^".ToString() +
                                                           ds.Tables[2].Rows[i]["BulkMRP"] + "^".ToString() +
                                                           ds.Tables[2].Rows[i]["CategoryName"] + "^".ToString() +
                                                           ds.Tables[2].Rows[i]["Points"] + "^".ToString() +
                                                           ds.Tables[2].Rows[i]["CategoryID"] + "^".ToString() +
                                                           ds.Tables[2].Rows[i]["ProductType"] + "".ToString(), ds.Tables[2].Rows[i]["ProductName"] + "(Rs = ".ToString() + Convert.ToDouble(ds.Tables[2].Rows[i]["BulkMRP"]).ToString("f") + ")".ToString());

                }


            }
            tvProducts.Nodes[0].Nodes.Add("Free product");
            if (strInvType != "Invoice" && strInvType != "InvoiceBultin")
            {
                if (ds.Tables[3].Rows.Count > 0)
                {


                    for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
                    {
                        tvProducts.Nodes[0].Nodes[3].Nodes.Add(ds.Tables[3].Rows[i]["ProductID"] + "^".ToString() +
                                                               ds.Tables[3].Rows[i]["SingleMRP"] + "^".ToString() +
                                                               ds.Tables[3].Rows[i]["BulkMRP"] + "^".ToString() +
                                                               ds.Tables[3].Rows[i]["CategoryName"] + "^".ToString() +
                                                               ds.Tables[3].Rows[i]["Points"] + "^".ToString() +
                                                               ds.Tables[3].Rows[i]["CategoryID"] + "^".ToString() +
                                                               ds.Tables[3].Rows[i]["ProductType"] + "".ToString(), ds.Tables[3].Rows[i]["ProductName"] + "(Rs = ".ToString() + ds.Tables[3].Rows[i]["SingleMRP"] + ")".ToString());
                    }


                }
            }
            this.tvProducts.SelectedNode = tNode.Nodes[1];
            this.tvProducts.SelectedNode.Expand();
        }
        private void FillFreeProducts()
        {
            objInv = new InvoiceDB();
            DataSet ds = new DataSet();
            ds = objInv.InvProductSearchCursor_Get(CommonData.CompanyCode);
            TreeNode tNode;
            tNode = tvProducts.Nodes.Add("Products");
            if (ds.Tables[3].Rows.Count > 0)
            {
                tvProducts.Nodes[0].Nodes.Add("Free product");

                for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
                {
                    tvProducts.Nodes[0].Nodes[0].Nodes.Add(ds.Tables[3].Rows[i]["ProductID"] + "^".ToString() +
                                                           ds.Tables[3].Rows[i]["SingleMRP"] + "^".ToString() +
                                                           ds.Tables[3].Rows[i]["BulkMRP"] + "^".ToString() +
                                                           ds.Tables[3].Rows[i]["CategoryName"] + "^".ToString() +
                                                           ds.Tables[3].Rows[i]["Points"] + "^".ToString() +
                                                           ds.Tables[3].Rows[i]["CategoryID"] + "^".ToString() +
                                                           ds.Tables[3].Rows[i]["ProductType"] + "".ToString(), ds.Tables[3].Rows[i]["ProductName"] + "(Rs = ".ToString() + ds.Tables[0].Rows[i]["SingleMRP"] + ")".ToString());
                }


            }
            this.tvProducts.SelectedNode = tNode.Nodes[0];
            this.tvProducts.SelectedNode.Expand();
        }
        private void FillDealerProducts()
        {
            objInv = new InvoiceDB();
            DataSet ds = new DataSet();
            ds = objInv.InvProductSearchCursor_Get(CommonData.CompanyCode);
            TreeNode tNode;
            tNode = tvProducts.Nodes.Add("Products");
            if (ds.Tables[0].Rows.Count > 0)
            {
                tvProducts.Nodes[0].Nodes.Add(ds.Tables[0].Rows[0]["CategoryName"]+"");
                string strCategory = ds.Tables[0].Rows[0]["CategoryName"] + "";
                int index=0;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {                   
                    if (strCategory != ds.Tables[0].Rows[i]["CategoryName"] + "")
                    {
                        index++;
                        strCategory = ds.Tables[0].Rows[i]["CategoryName"] + "";
                        tvProducts.Nodes[0].Nodes.Add(ds.Tables[0].Rows[i]["CategoryName"] + "");
                    }
                    tvProducts.Nodes[0].Nodes[index].Nodes.Add(ds.Tables[0].Rows[i]["ProductID"] + "^".ToString() +
                                                             ds.Tables[0].Rows[i]["SingleMRP"] + "^".ToString() +
                                                             ds.Tables[0].Rows[i]["BulkMRP"] + "^".ToString() +
                                                             ds.Tables[0].Rows[i]["CategoryName"] + "^".ToString() +
                                                             ds.Tables[0].Rows[i]["Points"] + "^".ToString() +
                                                             ds.Tables[0].Rows[i]["CategoryID"] + "^".ToString() +
                                                             ds.Tables[0].Rows[i]["ProductType"] + "".ToString(), ds.Tables[0].Rows[i]["ProductName"] + "(Rs = ".ToString() + ds.Tables[0].Rows[i]["SingleMRP"] + ")".ToString());
                }


            }
            this.tvProducts.SelectedNode = tNode.Nodes[0];
            this.tvProducts.SelectedNode.Expand();
        }
        private TreeNode SearchNode(string SearchText, TreeNode StartNode)
        {
            TreeNode node = null;
            while (StartNode != null)
            {
                if (StartNode.Text.ToLower().StartsWith(SearchText.ToLower()))
                {
                    node = StartNode;
                    break;
                };
                if (StartNode.Nodes.Count != 0)
                {
                    node = SearchNode(SearchText, StartNode.Nodes[0]);//Recursive Search
                    if (node != null)
                    {
                        break;
                    };
                };
                StartNode = StartNode.NextNode;
            };
            return node;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            AddItemsToGrid(tvProducts.Nodes[0]);
        }

        private void AddItemsToGrid(TreeNode StartNode)
        {
            //string[] strArrProduct = null;
            //bool isItemExisted = false;
            DataGridView dgvProducts = null;
            //int intRow = 0;
            if (CheckData())
            {
                if (strInvType == "Invoice")
                {
                    //dgvProducts = ((Invoice)objFrmInvoice).gvProductDetails;
                    AddItemsToGridInvoice(dgvProducts);
                }
                if (strInvType == "OpeningStock")
                {
                    //dgvProducts = ((OpeningStock)objFrmOpenStock).gvProductDetails;
                    AddItemsToGridOpeningStock(dgvProducts);
                }
                if (strInvType == "InvoiceBultin")
                {
                    //dgvProducts = ((InvoiceBultin)objFrmInvoiceBultin).gvProductDetails;
                    AddItemsToGridInvoice(dgvProducts);
                }

                if (strInvType == "SalesOrders")
                {
                    //dgvProducts = ((SalseORder)objFrmSalseOrder).gvProductDetails;
                    AddItemsToGridSalesOrders(dgvProducts);
                }
                if (strInvType == "DoorKnocks")
                {
                    //dgvProducts = ((DoorKnocks)objFrmDoorKnocks).gvProductDetails;
                    AddItemsToGridDoorKnocks(dgvProducts);
                }
                //if (strInvType == "FreeProducts1")
                //{
                //    dgvProducts = ((FreeProducts)objSaleProducts).gvProductDetails;
                //    AddItemsToGridSaleProducts(dgvProducts);
                //}
                if (strInvType == "FreeProducts2")
                {
                    //dgvProducts = ((FreeProducts)objFreeProducts).gvFreeProduct;
                    AddItemsToGridFreeProducts(dgvProducts);
                }
                if (strInvType == "InvoiceTemplateProducts")
                {
                    dgvProducts = ((InvoiceTemplateProducts)objInvoiceTemplateProducts).gvProductDetails;
                    AddItemsToGridInvoiceTemplateProducts(dgvProducts);
                }
                if (strInvType == "DC_PU")
                {
                    //dgvProducts = ((DeliveryChallanPU)objDeliveryChallanPU).gvReqDetails;
                    AddItemsToGridDeliveryChalanPU(dgvProducts);
                }
                if (strInvType == "DC_PU_PM")
                {
                    //dgvProducts = ((DeliveryChallanPU)objDeliveryChallanPU).gvPackingDetl;
                    AddItemsToGridDeliveryChalanPU_pm(dgvProducts);
                }
                if (strInvType == "StockTransfer")
                {
                    //dgvProducts = ((StockTransfer)objStockTransfer).gvProductDetails;
                    AddStockTrnsferItemsToGrid(dgvProducts);
                }
                if (strInvType == "SPRefill_Sours")
                {
                    //dgvProducts = ((SPRefill)objSPRefill).dgvSourceProd;
                    AddItemsToGridSPRefillProducts(dgvProducts);
                }
                if (strInvType == "SPRefill_dist")
                {
                    //dgvProducts = ((SPRefill)objSPRefill).dgvDistProd;
                    AddItemsToGridSPRefillProducts(dgvProducts);
                }
                if (strInvType == "INTERNALDAMAGE")
                {
                    //dgvProducts = ((InternalDamage)objInternalDamage).gvProductDetails;
                    AddItemsToGridInternalDamage(dgvProducts);
                }
                if (strInvType == "GRN")
                {
                    //dgvProducts = ((GoodsReceiptNotePU)objGoodsReceiptNotePU).gvReqDetails;
                    AddItemsToGridGoodsReceiptNote(dgvProducts);
                }
                if (strInvType == "GRN_PM")
                {
                    //dgvProducts = ((GoodsReceiptNotePU)objGoodsReceiptNotePU).gvPackingDetl;
                    AddItemsToGridGoodsReceiptNote_pm(dgvProducts);
                }
                if (strInvType == "ProductPriceCircular")
                {
                    //dgvProducts = ((ProductPriceCircular)objProductPriceCircular).gvProductDetails;
                    AddProductsToGrid(dgvProducts);
                }
                if (strInvType == "OrderBooking")
                {
                    dgvProducts = ((OrderBookingForm)objFrmOrderBooking).gvProductDetails;
                    AddProductsToGrid(dgvProducts);
                }
                if (strInvType == "StockPoint_DLDC")
                {
                    dgvProducts = ((DealerInvoice)objDealerInvoice).gvProductDetails;
                    AddProductsToGridDLDC(dgvProducts);
                }
            }
            else
            {
                MessageBox.Show("Select Product!", "Product Search");
            }
        }
        private void AddProductsToGrid(DataGridView dgvProducts)
        {
            string[] strArrProduct = null;
            bool isItemExisted = false;
            int intRow = 0;
            intRow = dgvProducts.Rows.Count + 1;
            
            if (CheckData())
            {
                for (int i = 0; i < tvProducts.Nodes[0].Nodes.Count; i++)
                {
                    for (int j = 0; j < tvProducts.Nodes[0].Nodes[i].Nodes.Count; j++)
                    {
                        if (tvProducts.Nodes[0].Nodes[i].Nodes[j].Checked == true)
                        {
                            string strProduct = tvProducts.Nodes[0].Nodes[i].Nodes[j].Name.ToString();//((NewCheckboxListItem)(clbProduct.Items[i])).Tag;
                            strArrProduct = strProduct.Split('^');
                            isItemExisted = false;
                            for (int nRow = 0; nRow < dgvProducts.Rows.Count; nRow++)
                            {
                                if (dgvProducts.Rows[nRow].Cells["ProductId"].Value.ToString().Trim() == strArrProduct[0].ToString().Trim())
                                {
                                    isItemExisted = true;
                                    break;
                                }
                            }
                            if ((Convert.ToDouble(strArrProduct[2]) > 0) && isItemExisted == false)  //dt.Rows[i]["pm_single_mrp"] or dt.Rows[i]["PM_BULK_MRP"]
                            {
                                DataGridViewRow tempRow = new DataGridViewRow();
                                DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                                cellSLNO.Value = intRow;
                                tempRow.Cells.Add(cellSLNO);



                                DataGridViewCell cellProductId = new DataGridViewTextBoxCell();
                                //cellProductId.Value = dt.Rows[i]["cellProductId"];
                                cellProductId.Value = strArrProduct[0].ToString();
                                tempRow.Cells.Add(cellProductId);

                                DataGridViewCell cellProductName = new DataGridViewTextBoxCell();
                                //cellProductName.Value = dt.Rows[i]["ProductName"];
                                cellProductName.Value = tvProducts.Nodes[0].Nodes[i].Nodes[j].Text;
                                tempRow.Cells.Add(cellProductName);


                                DataGridViewCell cellCategoryName = new DataGridViewTextBoxCell();
                                //cellCategoryName.Value = dt.Rows[i]["CategoryName"];
                                cellCategoryName.Value = strArrProduct[3].ToString();
                                tempRow.Cells.Add(cellCategoryName);

                                DataGridViewCell cellDesc = new DataGridViewTextBoxCell();
                                //cellCategoryName.Value = dt.Rows[i]["CategoryName"];
                                cellDesc.Value = strArrProduct[3].ToString();
                                tempRow.Cells.Add(cellDesc);

                                DataGridViewCell cellQTY = new DataGridViewTextBoxCell();
                                cellQTY.Value = "0";
                                tempRow.Cells.Add(cellQTY);

                                DataGridViewCell cellprodMrp = new DataGridViewTextBoxCell();
                                cellprodMrp.Value = Convert.ToDouble(strArrProduct[2]).ToString("f");
                                //cellprodMrp.Value = dt.Rows[i]["BulkMRP"];
                                tempRow.Cells.Add(cellprodMrp);

                                DataGridViewCell cellAmt = new DataGridViewTextBoxCell();
                                cellAmt.Value = Convert.ToDouble("0").ToString("f");
                                tempRow.Cells.Add(cellAmt);


                                //DataGridViewCell cellOfferPrice = new DataGridViewTextBoxCell();
                                //cellOfferPrice.Value = Convert.ToDouble(strArrProduct[2]).ToString("f");
                                ////cellOfferPrice.Value = dt.Rows[i]["BulkMRP"];
                                //tempRow.Cells.Add(cellOfferPrice);

                                //DataGridViewCell cellProdPoints = new DataGridViewTextBoxCell();
                                //cellProdPoints.Value = Convert.ToDouble(strArrProduct[5]).ToString("f");
                                ////cellProdPoints.Value = dt.Rows[i]["Points"];
                                //tempRow.Cells.Add(cellProdPoints);



                                intRow = intRow + 1;
                                dgvProducts.Rows.Add(tempRow);

                                if (strArrProduct[4] == "COMBI PACKS")
                                {
                                    DataGridView dgvSubProducts = null;
                                    //dgvSubProducts = ((ProductPriceCircular)objProductPriceCircular).gvSubProdDetails;
                                    AddSubproductsToGrid(dgvSubProducts, strArrProduct[0].ToString());

                                }
                            }


                        }
                    }
                }
                this.Close();
            }           

        }
        private void AddProductsToGridDLDC(DataGridView dgvProducts)
        {
            string[] strArrProduct = null;
            bool isItemExisted = false;
            int intRow = 0;
            intRow = dgvProducts.Rows.Count + 1;

            if (CheckData())
            {
                for (int i = 0; i < tvProducts.Nodes[0].Nodes.Count; i++)
                {
                    for (int j = 0; j < tvProducts.Nodes[0].Nodes[i].Nodes.Count; j++)
                    {
                        if (tvProducts.Nodes[0].Nodes[i].Nodes[j].Checked == true)
                        {
                            string strProduct = tvProducts.Nodes[0].Nodes[i].Nodes[j].Name.ToString();//((NewCheckboxListItem)(clbProduct.Items[i])).Tag;
                            strArrProduct = strProduct.Split('^');
                            isItemExisted = false;
                            for (int nRow = 0; nRow < dgvProducts.Rows.Count; nRow++)
                            {
                                if (dgvProducts.Rows[nRow].Cells["ProductId"].Value.ToString().Trim() == strArrProduct[0].ToString().Trim())
                                {
                                    isItemExisted = true;
                                    break;
                                }
                            }
                            if ((Convert.ToDouble(strArrProduct[2]) > 0) && isItemExisted == false)  //dt.Rows[i]["pm_single_mrp"] or dt.Rows[i]["PM_BULK_MRP"]
                            {
                                DataGridViewRow tempRow = new DataGridViewRow();
                                DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                                cellSLNO.Value = intRow;
                                tempRow.Cells.Add(cellSLNO);



                                DataGridViewCell cellProductId = new DataGridViewTextBoxCell();
                                //cellProductId.Value = dt.Rows[i]["cellProductId"];
                                cellProductId.Value = strArrProduct[0].ToString();
                                tempRow.Cells.Add(cellProductId);

                                DataGridViewCell cellProductName = new DataGridViewTextBoxCell();
                                //cellProductName.Value = dt.Rows[i]["ProductName"];
                                cellProductName.Value = tvProducts.Nodes[0].Nodes[i].Nodes[j].Text;
                                tempRow.Cells.Add(cellProductName);


                                DataGridViewCell cellCategoryName = new DataGridViewTextBoxCell();
                                //cellCategoryName.Value = dt.Rows[i]["CategoryName"];
                                cellCategoryName.Value = strArrProduct[3].ToString();
                                tempRow.Cells.Add(cellCategoryName);

                                DataGridViewCell cellBatchNo = new DataGridViewTextBoxCell();
                                //cellCategoryName.Value = dt.Rows[i]["CategoryName"];
                                cellBatchNo.Value = "GEN";
                                tempRow.Cells.Add(cellBatchNo);                             

                                DataGridViewCell cellQTY = new DataGridViewTextBoxCell();
                                cellQTY.Value = "0";
                                tempRow.Cells.Add(cellQTY);

                                DataGridViewCell cellprodMrp = new DataGridViewTextBoxCell();
                                cellprodMrp.Value = Convert.ToDouble(strArrProduct[2]).ToString("f");
                                //cellprodMrp.Value = dt.Rows[i]["BulkMRP"];
                                tempRow.Cells.Add(cellprodMrp);

                                DataGridViewCell cellAmt = new DataGridViewTextBoxCell();
                                cellAmt.Value = Convert.ToDouble("0").ToString("f");
                                tempRow.Cells.Add(cellAmt);                                

                                DataGridViewCell cellDiscPer = new DataGridViewTextBoxCell();
                                cellDiscPer.Value = Convert.ToDouble("0").ToString("f");
                                tempRow.Cells.Add(cellDiscPer);

                                DataGridViewCell cellDiscAmt = new DataGridViewTextBoxCell();
                                cellDiscAmt.Value = Convert.ToDouble("0").ToString("f");
                                tempRow.Cells.Add(cellDiscAmt);

                                DataGridViewCell cellVatPer = new DataGridViewTextBoxCell();
                                cellVatPer.Value = ((DealerInvoice)objDealerInvoice).txtVatPers.Text;
                                //cellVatPer.Value = Convert.ToDouble("0").ToString("f");
                                tempRow.Cells.Add(cellVatPer);

                                DataGridViewCell cellVatAmt = new DataGridViewTextBoxCell();
                                cellVatAmt.Value = Convert.ToDouble("0").ToString("f");
                                tempRow.Cells.Add(cellVatAmt);
                                DataGridViewCell cellNetAmt = new DataGridViewTextBoxCell();
                                cellNetAmt.Value = Convert.ToDouble("0").ToString("f");
                                tempRow.Cells.Add(cellNetAmt);




                                intRow = intRow + 1;
                                dgvProducts.Rows.Add(tempRow);

                                //if (strArrProduct[4] == "COMBI PACKS")
                                //{
                                //    DataGridView dgvSubProducts = null;
                                //    //dgvSubProducts = ((ProductPriceCircular)objProductPriceCircular).gvSubProdDetails;
                                //    AddSubproductsToGrid(dgvSubProducts, strArrProduct[0].ToString());

                                //}
                            }


                        }
                    }
                }
                this.Close();
            }           

        }
        private void AddSubproductsToGrid(DataGridView dgvSubProducts, string sCombiID)
        {
            objSQLDb = new SQLDB();
            DataTable dt = new DataTable();

            string strCommand = "";
            int intRow = 0;

            intRow = dgvSubProducts.Rows.Count + 1;


            strCommand = "SELECT PML_PRODUCT_ID CombiID" +
                        ",PC.PM_PRODUCT_NAME CombiName" +
                        ",PML_SNGLPCK_PRODUCT_ID SingleProdID" +
                        ",PM.PM_PRODUCT_NAME SingleProdName" +
                        ",CM.CATEGORY_NAME SingleCategory" +
                        ",PML_QTY SingleQty " +
                        ",PML_PRODUCT_SL_NO " +
                        "FROM PRODUCT_MAS_LI " +
                        "INNER JOIN PRODUCT_MAS PM " +
                        "ON PM.PM_PRODUCT_ID = PML_SNGLPCK_PRODUCT_ID " +
                        "INNER JOIN CATEGORY_MASTER CM " +
                        "ON CM.CATEGORY_ID = PM.PM_CATEGORY_ID " +
                        "INNER JOIN PRODUCT_MAS PC " +
                        "ON PC.PM_PRODUCT_ID = PML_PRODUCT_ID " +
                        "WHERE PML_PRODUCT_ID = '" + sCombiID + "'";

            dt = objSQLDb.ExecuteDataSet(strCommand).Tables[0];


            for (int k = 0; k < dt.Rows.Count; k++)
            {
                DataGridViewRow tempRow = new DataGridViewRow();
                DataGridViewCell cellSLNO1 = new DataGridViewTextBoxCell();
                cellSLNO1.Value = intRow;
                intRow = intRow + 1;
                tempRow.Cells.Add(cellSLNO1);

                DataGridViewCell cellProductId = new DataGridViewTextBoxCell();
                //cellProductId.Value = dt.Rows[i]["cellProductId"];
                cellProductId.Value = sCombiID;
                tempRow.Cells.Add(cellProductId);

                DataGridViewCell cellProductName = new DataGridViewTextBoxCell();
                //cellProductName.Value = dt.Rows[i]["ProductName"];
                cellProductName.Value = dt.Rows[k]["CombiName"];
                tempRow.Cells.Add(cellProductName);

                DataGridViewCell cellCategoryName = new DataGridViewTextBoxCell();
                //cellCategoryName.Value = dt.Rows[i]["CategoryName"];
                cellCategoryName.Value = dt.Rows[k]["SingleCategory"];
                tempRow.Cells.Add(cellCategoryName);

                DataGridViewCell cellSingleProdId = new DataGridViewTextBoxCell();
                cellSingleProdId.Value = dt.Rows[k]["SingleProdID"];
                tempRow.Cells.Add(cellSingleProdId);

                DataGridViewCell cellSingleProdName = new DataGridViewTextBoxCell();
                cellSingleProdName.Value = dt.Rows[k]["SingleProdName"];
                tempRow.Cells.Add(cellSingleProdName);

                DataGridViewCell cellProdQty = new DataGridViewTextBoxCell();
                cellProdQty.Value = dt.Rows[k]["SingleQty"];
                tempRow.Cells.Add(cellProdQty);

                DataGridViewCell cellProdMrp = new DataGridViewTextBoxCell();
                cellProdMrp.Value = "0";
                tempRow.Cells.Add(cellProdMrp);

                DataGridViewCell cellProdOfferPrice = new DataGridViewTextBoxCell();
                cellProdOfferPrice.Value = "0";
                tempRow.Cells.Add(cellProdOfferPrice);

                DataGridViewCell cellProdPoints = new DataGridViewTextBoxCell();
                cellProdPoints.Value = "0";
                tempRow.Cells.Add(cellProdPoints);

                DataGridViewCell cellProdSLNo = new DataGridViewTextBoxCell();
                cellProdSLNo.Value = dt.Rows[k]["PML_PRODUCT_SL_NO"]; ;
                tempRow.Cells.Add(cellProdSLNo);

                dgvSubProducts.Rows.Add(tempRow);


            }
        }

        private void AddItemsToGridGoodsReceiptNote(DataGridView dgvProducts)
        {
            string[] strArrProduct = null;
            bool isItemExisted = false;
            int intRow = 0;

            intRow = dgvProducts.Rows.Count + 1;

            if (CheckData())
            {

                for (int i = 0; i < tvProducts.Nodes[0].Nodes.Count; i++)
                {
                    for (int j = 0; j < tvProducts.Nodes[0].Nodes[i].Nodes.Count; j++)
                    {
                        if (tvProducts.Nodes[0].Nodes[i].Nodes[j].Checked == true)
                        {
                            string strProduct = tvProducts.Nodes[0].Nodes[i].Nodes[j].Name.ToString();//((NewCheckboxListItem)(clbProduct.Items[i])).Tag;
                            strArrProduct = strProduct.Split('^');
                            for (int nRow = 0; nRow < dgvProducts.Rows.Count; nRow++)
                            {
                                if (dgvProducts.Rows[nRow].Cells["ProductID"].Value.ToString().Trim() == strArrProduct[0].ToString().Trim())
                                {
                                    isItemExisted = true;
                                    break;
                                }
                            }
                            if ((Convert.ToDouble(strArrProduct[1]) > 0 || (Convert.ToDouble(strArrProduct[1]) > 0)) && isItemExisted == false)  //dt.Rows[i]["pm_single_mrp"] or dt.Rows[i]["PM_BULK_MRP"]
                            {
                                DataGridViewRow tempRow = new DataGridViewRow();
                                DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                                cellSLNO.Value = intRow;
                                tempRow.Cells.Add(cellSLNO);

                                DataGridViewCell cellMainProductID = new DataGridViewTextBoxCell();
                                //cellMainProductID.Value = dt.Rows[i]["pm_product_id"];
                                cellMainProductID.Value = strArrProduct[0];
                                tempRow.Cells.Add(cellMainProductID);

                                DataGridViewCell cellMainProduct = new DataGridViewTextBoxCell();
                                //cellMainProduct.Value = dt.Rows[i]["pm_product_name"];
                                cellMainProduct.Value = strArrProduct[3];
                                //tvProducts.Nodes[0].Nodes[i].Nodes[j].Text;//strArrProduct[3];
                                tempRow.Cells.Add(cellMainProduct);

                                DataGridViewCell cellSubProduct = new DataGridViewTextBoxCell();
                                //cellSubProduct.Value = dt.Rows[i]["category_name"];
                                cellSubProduct.Value = tvProducts.Nodes[0].Nodes[i].Nodes[j].Text;
                                tempRow.Cells.Add(cellSubProduct);


                                DataGridViewCell cellDessc = new DataGridViewTextBoxCell();
                                //cellDessc.Value = dt.Rows[i]["pm_product_name"];
                                cellDessc.Value = "GENERAL";
                                tempRow.Cells.Add(cellDessc);

                                DataGridViewCell cellQTY = new DataGridViewTextBoxCell();
                                cellQTY.Value = "0";
                                tempRow.Cells.Add(cellQTY);

                                DataGridViewCell cellRQTY = new DataGridViewTextBoxCell();
                                cellRQTY.Value = "";
                                tempRow.Cells.Add(cellRQTY);

                                DataGridViewCell cellBQTY = new DataGridViewTextBoxCell();
                                cellBQTY.Value = "";
                                tempRow.Cells.Add(cellBQTY);


                                DataGridViewCell cellRate = new DataGridViewTextBoxCell();

                                if (strArrProduct[5].ToString() == "009") //Combi packs
                                {
                                    //cellRate.Value = dt.Rows[i]["pm_bulk_mrp"];  
                                    cellRate.Value = Convert.ToDouble(strArrProduct[2]).ToString("f");
                                    tempRow.Cells.Add(cellRate);
                                }
                                else
                                {
                                    //cellRate.Value = dt.Rows[i]["pm_single_mrp"];
                                    cellRate.Value = Convert.ToDouble(strArrProduct[1]).ToString("f");
                                    tempRow.Cells.Add(cellRate);
                                }

                                if (strArrProduct[6] == "Free")
                                    cellRate.Value = "00.01";

                                DataGridViewCell cellPoints = new DataGridViewTextBoxCell();
                                //cellPoints.Value = dt.Rows[i]["TIPPoints"];
                                cellPoints.Value = Convert.ToDouble(strArrProduct[4]).ToString("f");
                                tempRow.Cells.Add(cellPoints);

                                intRow = intRow + 1;
                                dgvProducts.Rows.Add(tempRow);

                            }
                        }
                    }
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Select Product!", "Product Search");
            }
        }
        private void AddItemsToGridGoodsReceiptNote_pm(DataGridView dgvProducts)
        {
            string[] strArrProduct = null;
            bool isItemExisted = false;
            int intRow = 0;

            intRow = dgvProducts.Rows.Count + 1;

            if (CheckData())
            {

                for (int i = 0; i < tvProducts.Nodes[0].Nodes.Count; i++)
                {
                    for (int j = 0; j < tvProducts.Nodes[0].Nodes[i].Nodes.Count; j++)
                    {
                        if (tvProducts.Nodes[0].Nodes[i].Nodes[j].Checked == true)
                        {
                            string strProduct = tvProducts.Nodes[0].Nodes[i].Nodes[j].Name.ToString();//((NewCheckboxListItem)(clbProduct.Items[i])).Tag;
                            strArrProduct = strProduct.Split('^');
                            for (int nRow = 0; nRow < dgvProducts.Rows.Count; nRow++)
                            {
                                if (dgvProducts.Rows[nRow].Cells["ProductID1"].Value.ToString().Trim() == strArrProduct[0].ToString().Trim())
                                {
                                    isItemExisted = true;
                                    break;
                                }
                            }
                            if ((Convert.ToDouble(strArrProduct[1]) > 0 || (Convert.ToDouble(strArrProduct[1]) > 0)) && isItemExisted == false)  //dt.Rows[i]["pm_single_mrp"] or dt.Rows[i]["PM_BULK_MRP"]
                            {
                                DataGridViewRow tempRow = new DataGridViewRow();
                                DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                                cellSLNO.Value = intRow;
                                tempRow.Cells.Add(cellSLNO);

                                DataGridViewCell cellMainProductID = new DataGridViewTextBoxCell();
                                //cellMainProductID.Value = dt.Rows[i]["pm_product_id"];
                                cellMainProductID.Value = strArrProduct[0];
                                tempRow.Cells.Add(cellMainProductID);

                                DataGridViewCell cellMainProduct = new DataGridViewTextBoxCell();
                                //cellMainProduct.Value = dt.Rows[i]["pm_product_name"];
                                cellMainProduct.Value = strArrProduct[3];
                                //tvProducts.Nodes[0].Nodes[i].Nodes[j].Text;//strArrProduct[3];
                                tempRow.Cells.Add(cellMainProduct);

                                DataGridViewCell cellSubProduct = new DataGridViewTextBoxCell();
                                //cellSubProduct.Value = dt.Rows[i]["category_name"];
                                cellSubProduct.Value = tvProducts.Nodes[0].Nodes[i].Nodes[j].Text;
                                tempRow.Cells.Add(cellSubProduct);


                                DataGridViewCell cellDessc = new DataGridViewTextBoxCell();
                                //cellDessc.Value = dt.Rows[i]["pm_product_name"];
                                cellDessc.Value = "GENERAL";
                                tempRow.Cells.Add(cellDessc);

                                DataGridViewCell cellQTY = new DataGridViewTextBoxCell();
                                cellQTY.Value = "0";
                                tempRow.Cells.Add(cellQTY);

                                DataGridViewCell cellRQTY = new DataGridViewTextBoxCell();
                                cellRQTY.Value = "";
                                tempRow.Cells.Add(cellRQTY);

                                DataGridViewCell cellBQTY = new DataGridViewTextBoxCell();
                                cellBQTY.Value = "";
                                tempRow.Cells.Add(cellBQTY);

                                DataGridViewCell cellRate = new DataGridViewTextBoxCell();
                                cellRate.Value = "0";
                                tempRow.Cells.Add(cellRate);

                                DataGridViewCell cellPoints = new DataGridViewTextBoxCell();
                                //cellPoints.Value = dt.Rows[i]["TIPPoints"];
                                cellPoints.Value = Convert.ToDouble(strArrProduct[4]).ToString("f");
                                tempRow.Cells.Add(cellPoints);

                                intRow = intRow + 1;
                                dgvProducts.Rows.Add(tempRow);

                            }
                        }
                    }
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Select Product!", "Product Search");
            }
        }


        private void AddStockTrnsferItemsToGrid(DataGridView dgvProducts)
        {
            string[] strArrProduct = null;
            bool isItemExisted = false;
            int intRow = 0;
            intRow = dgvProducts.Rows.Count + 1;
            if (CheckData())
            {
                for (int i = 0; i < tvProducts.Nodes[0].Nodes.Count; i++)
                {
                    for (int j = 0; j < tvProducts.Nodes[0].Nodes[i].Nodes.Count; j++)
                    {
                        if (tvProducts.Nodes[0].Nodes[i].Nodes[j].Checked == true)
                        {
                            string strProduct = tvProducts.Nodes[0].Nodes[i].Nodes[j].Name.ToString();//((NewCheckboxListItem)(clbProduct.Items[i])).Tag;
                            strArrProduct = strProduct.Split('^');
                            for (int nRow = 0; nRow < dgvProducts.Rows.Count; nRow++)
                            {
                                if (dgvProducts.Rows[nRow].Cells["ProductID"].Value.ToString().Trim() == strArrProduct[0].ToString().Trim())
                                {
                                    isItemExisted = true;
                                    break;
                                }
                            }
                            if ((Convert.ToDouble(strArrProduct[2]) > 0) && isItemExisted == false)  //dt.Rows[i]["pm_single_mrp"] or dt.Rows[i]["PM_BULK_MRP"]
                            {
                                DataGridViewRow tempRow = new DataGridViewRow();
                                DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                                cellSLNO.Value = intRow;
                                tempRow.Cells.Add(cellSLNO);

                                DataGridViewCell cellMainProductID = new DataGridViewTextBoxCell();
                                //cellMainProductID.Value = dt.Rows[i]["pm_product_id"];
                                cellMainProductID.Value = strArrProduct[0].ToString();
                                tempRow.Cells.Add(cellMainProductID);

                                DataGridViewCell cellMainProduct = new DataGridViewTextBoxCell();
                                //cellMainProduct.Value = dt.Rows[i]["pm_product_name"];
                                cellMainProduct.Value = strArrProduct[1].ToString();
                                tempRow.Cells.Add(cellMainProduct);

                                DataGridViewCell cellPoints = new DataGridViewTextBoxCell();
                                cellPoints.Value = Convert.ToDouble(strArrProduct[2]).ToString("f");
                                tempRow.Cells.Add(cellPoints);

                                intRow = intRow + 1;
                                dgvProducts.Rows.Add(tempRow);
                            }
                        }
                    }
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Select Product!", "Product Search");
            }
        }

        private void AddItemsToGridOpeningStock(DataGridView dgvProducts)
        {
            string[] strArrProduct = null;
            bool isItemExisted = false;
            int intRow = 0;

            intRow = dgvProducts.Rows.Count + 1;

            if (CheckData())
            {

                for (int i = 0; i < tvProducts.Nodes[0].Nodes.Count; i++)
                {
                    for (int j = 0; j < tvProducts.Nodes[0].Nodes[i].Nodes.Count; j++)
                    {
                        if (tvProducts.Nodes[0].Nodes[i].Nodes[j].Checked == true)
                        {
                            string strProduct = tvProducts.Nodes[0].Nodes[i].Nodes[j].Name.ToString();//((NewCheckboxListItem)(clbProduct.Items[i])).Tag;
                            strArrProduct = strProduct.Split('^');
                            for (int nRow = 0; nRow < dgvProducts.Rows.Count; nRow++)
                            {
                                if (dgvProducts.Rows[nRow].Cells["ProductID"].Value.ToString().Trim() == strArrProduct[0].ToString().Trim())
                                {
                                    isItemExisted = true;
                                    break;
                                }
                            }
                            if ((Convert.ToDouble(strArrProduct[1]) > 0 || (Convert.ToDouble(strArrProduct[1]) > 0)) && isItemExisted == false)  //dt.Rows[i]["pm_single_mrp"] or dt.Rows[i]["PM_BULK_MRP"]
                            {
                                DataGridViewRow tempRow = new DataGridViewRow();
                                DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                                cellSLNO.Value = intRow;
                                tempRow.Cells.Add(cellSLNO);

                                DataGridViewCell cellMainProductID = new DataGridViewTextBoxCell();
                                //cellMainProductID.Value = dt.Rows[i]["pm_product_id"];
                                cellMainProductID.Value = strArrProduct[0];
                                tempRow.Cells.Add(cellMainProductID);

                                DataGridViewCell cellSubProduct = new DataGridViewTextBoxCell();
                                //cellSubProduct.Value = dt.Rows[i]["category_name"];
                                cellSubProduct.Value = tvProducts.Nodes[0].Nodes[i].Nodes[j].Text;
                                tempRow.Cells.Add(cellSubProduct);

                                DataGridViewCell cellMainProduct = new DataGridViewTextBoxCell();
                                //cellMainProduct.Value = dt.Rows[i]["pm_product_name"];
                                cellMainProduct.Value = strArrProduct[3];
                                //tvProducts.Nodes[0].Nodes[i].Nodes[j].Text;//strArrProduct[3];
                                tempRow.Cells.Add(cellMainProduct);

                                DataGridViewCell cellQTY = new DataGridViewTextBoxCell();
                                cellQTY.Value = "0";
                                tempRow.Cells.Add(cellQTY);

                                DataGridViewCell cellShortQTY = new DataGridViewTextBoxCell();
                                cellShortQTY.Value = "0";
                                tempRow.Cells.Add(cellShortQTY);

                                intRow = intRow + 1;
                                dgvProducts.Rows.Add(tempRow);

                            }
                        }
                    }
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Select Product!", "Product Search");
            }
        }

        private void AddItemsToGridDeliveryChalanPU(DataGridView dgvProducts)
        {
            string[] strArrProduct = null;
            bool isItemExisted = false;
            int intRow = 0;

            intRow = dgvProducts.Rows.Count + 1;

            if (CheckData())
            {

                for (int i = 0; i < tvProducts.Nodes[0].Nodes.Count; i++)
                {
                    for (int j = 0; j < tvProducts.Nodes[0].Nodes[i].Nodes.Count; j++)
                    {
                        if (tvProducts.Nodes[0].Nodes[i].Nodes[j].Checked == true)
                        {
                            string strProduct = tvProducts.Nodes[0].Nodes[i].Nodes[j].Name.ToString();//((NewCheckboxListItem)(clbProduct.Items[i])).Tag;
                            strArrProduct = strProduct.Split('^');
                            for (int nRow = 0; nRow < dgvProducts.Rows.Count; nRow++)
                            {
                                if (dgvProducts.Rows[nRow].Cells["ProductID"].Value.ToString().Trim() == strArrProduct[0].ToString().Trim())
                                {
                                    isItemExisted = true;
                                    break;
                                }
                            }
                            if ((Convert.ToDouble(strArrProduct[1]) > 0 || (Convert.ToDouble(strArrProduct[1]) > 0)) && isItemExisted == false)  //dt.Rows[i]["pm_single_mrp"] or dt.Rows[i]["PM_BULK_MRP"]
                            {
                                DataGridViewRow tempRow = new DataGridViewRow();
                                DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                                cellSLNO.Value = intRow;
                                tempRow.Cells.Add(cellSLNO);

                                DataGridViewCell cellMainProductID = new DataGridViewTextBoxCell();
                                //cellMainProductID.Value = dt.Rows[i]["pm_product_id"];
                                cellMainProductID.Value = strArrProduct[0];
                                tempRow.Cells.Add(cellMainProductID);

                                DataGridViewCell cellMainProduct = new DataGridViewTextBoxCell();
                                //cellMainProduct.Value = dt.Rows[i]["pm_product_name"];
                                cellMainProduct.Value = strArrProduct[3];
                                //tvProducts.Nodes[0].Nodes[i].Nodes[j].Text;//strArrProduct[3];
                                tempRow.Cells.Add(cellMainProduct);

                                DataGridViewCell cellSubProduct = new DataGridViewTextBoxCell();
                                //cellSubProduct.Value = dt.Rows[i]["category_name"];
                                cellSubProduct.Value = tvProducts.Nodes[0].Nodes[i].Nodes[j].Text;
                                tempRow.Cells.Add(cellSubProduct);


                                DataGridViewCell cellDessc = new DataGridViewTextBoxCell();
                                //cellDessc.Value = dt.Rows[i]["pm_product_name"];
                                cellDessc.Value = "GENERAL";
                                tempRow.Cells.Add(cellDessc);

                                DataGridViewCell cellQTY = new DataGridViewTextBoxCell();
                                cellQTY.Value = "0";
                                tempRow.Cells.Add(cellQTY);

                                DataGridViewCell cellRQTY = new DataGridViewTextBoxCell();
                                cellRQTY.Value = "";
                                tempRow.Cells.Add(cellRQTY);

                                DataGridViewCell cellBQTY = new DataGridViewTextBoxCell();
                                cellBQTY.Value = "";
                                tempRow.Cells.Add(cellBQTY);


                                DataGridViewCell cellRate = new DataGridViewTextBoxCell();

                                if (strArrProduct[5].ToString() == "009") //Combi packs
                                {
                                    //cellRate.Value = dt.Rows[i]["pm_bulk_mrp"];  
                                    cellRate.Value = Convert.ToDouble(strArrProduct[2]).ToString("f");
                                    tempRow.Cells.Add(cellRate);
                                }
                                else
                                {
                                    //cellRate.Value = dt.Rows[i]["pm_single_mrp"];
                                    cellRate.Value = Convert.ToDouble(strArrProduct[1]).ToString("f");
                                    tempRow.Cells.Add(cellRate);
                                }

                                if (strArrProduct[6] == "Free")
                                    cellRate.Value = "00.01";

                                DataGridViewCell cellPoints = new DataGridViewTextBoxCell();
                                //cellPoints.Value = dt.Rows[i]["TIPPoints"];
                                cellPoints.Value = Convert.ToDouble(strArrProduct[4]).ToString("f");
                                tempRow.Cells.Add(cellPoints);

                                intRow = intRow + 1;
                                dgvProducts.Rows.Add(tempRow);

                            }
                        }
                    }
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Select Product!", "Product Search");
            }
        }

        private void AddItemsToGridDeliveryChalanPU_pm(DataGridView dgvProducts)
        {
            string[] strArrProduct = null;
            bool isItemExisted = false;
            int intRow = 0;

            intRow = dgvProducts.Rows.Count + 1;

            if (CheckData())
            {

                for (int i = 0; i < tvProducts.Nodes[0].Nodes.Count; i++)
                {
                    for (int j = 0; j < tvProducts.Nodes[0].Nodes[i].Nodes.Count; j++)
                    {
                        if (tvProducts.Nodes[0].Nodes[i].Nodes[j].Checked == true)
                        {
                            string strProduct = tvProducts.Nodes[0].Nodes[i].Nodes[j].Name.ToString();//((NewCheckboxListItem)(clbProduct.Items[i])).Tag;
                            strArrProduct = strProduct.Split('^');
                            for (int nRow = 0; nRow < dgvProducts.Rows.Count; nRow++)
                            {
                                if (dgvProducts.Rows[nRow].Cells["ProductID1"].Value.ToString().Trim() == strArrProduct[0].ToString().Trim())
                                {
                                    isItemExisted = true;
                                    break;
                                }
                            }
                            if ((Convert.ToDouble(strArrProduct[1]) > 0 || (Convert.ToDouble(strArrProduct[1]) > 0)) && isItemExisted == false)  //dt.Rows[i]["pm_single_mrp"] or dt.Rows[i]["PM_BULK_MRP"]
                            {
                                DataGridViewRow tempRow = new DataGridViewRow();
                                DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                                cellSLNO.Value = intRow;
                                tempRow.Cells.Add(cellSLNO);

                                DataGridViewCell cellMainProductID = new DataGridViewTextBoxCell();
                                //cellMainProductID.Value = dt.Rows[i]["pm_product_id"];
                                cellMainProductID.Value = strArrProduct[0];
                                tempRow.Cells.Add(cellMainProductID);

                                DataGridViewCell cellMainProduct = new DataGridViewTextBoxCell();
                                //cellMainProduct.Value = dt.Rows[i]["pm_product_name"];
                                cellMainProduct.Value = strArrProduct[3];
                                //tvProducts.Nodes[0].Nodes[i].Nodes[j].Text;//strArrProduct[3];
                                tempRow.Cells.Add(cellMainProduct);

                                DataGridViewCell cellSubProduct = new DataGridViewTextBoxCell();
                                //cellSubProduct.Value = dt.Rows[i]["category_name"];
                                cellSubProduct.Value = tvProducts.Nodes[0].Nodes[i].Nodes[j].Text;
                                tempRow.Cells.Add(cellSubProduct);


                                DataGridViewCell cellDessc = new DataGridViewTextBoxCell();
                                //cellDessc.Value = dt.Rows[i]["pm_product_name"];
                                cellDessc.Value = "GENERAL";
                                tempRow.Cells.Add(cellDessc);

                                DataGridViewCell cellQTY = new DataGridViewTextBoxCell();
                                cellQTY.Value = "0";
                                tempRow.Cells.Add(cellQTY);

                                DataGridViewCell cellRQTY = new DataGridViewTextBoxCell();
                                cellRQTY.Value = "";
                                tempRow.Cells.Add(cellRQTY);

                                DataGridViewCell cellBQTY = new DataGridViewTextBoxCell();
                                cellBQTY.Value = "";
                                tempRow.Cells.Add(cellBQTY);

                                DataGridViewCell cellRate = new DataGridViewTextBoxCell();                                                              
                                cellRate.Value = "0";
                                tempRow.Cells.Add(cellRate);

                                DataGridViewCell cellPoints = new DataGridViewTextBoxCell();
                                //cellPoints.Value = dt.Rows[i]["TIPPoints"];
                                cellPoints.Value = Convert.ToDouble(strArrProduct[4]).ToString("f");
                                tempRow.Cells.Add(cellPoints);

                                intRow = intRow + 1;
                                dgvProducts.Rows.Add(tempRow);

                            }
                        }
                    }
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Select Product!", "Product Search");
            }
        }

        private void AddItemsToGridInvoiceTemplateProducts(DataGridView dgvProducts)
        {
            string[] strArrProduct = null;
            bool isItemExisted = false;
            int intRow = 0;

            intRow = dgvProducts.Rows.Count + 1;

            if (CheckData())
            {

                for (int i = 0; i < tvProducts.Nodes[0].Nodes.Count; i++)
                {
                    for (int j = 0; j < tvProducts.Nodes[0].Nodes[i].Nodes.Count; j++)
                    {
                        if (tvProducts.Nodes[0].Nodes[i].Nodes[j].Checked == true)
                        {
                            string strProduct = tvProducts.Nodes[0].Nodes[i].Nodes[j].Name.ToString();//((NewCheckboxListItem)(clbProduct.Items[i])).Tag;
                            strArrProduct = strProduct.Split('^');
                            for (int nRow = 0; nRow < dgvProducts.Rows.Count; nRow++)
                            {
                                if (dgvProducts.Rows[nRow].Cells["ProductID"].Value.ToString().Trim() == strArrProduct[0].ToString().Trim())
                                {
                                    isItemExisted = true;
                                    break;
                                }
                            }
                            if ((Convert.ToDouble(strArrProduct[1]) > 0 || (Convert.ToDouble(strArrProduct[1]) > 0)) && isItemExisted == false)  //dt.Rows[i]["pm_single_mrp"] or dt.Rows[i]["PM_BULK_MRP"]
                            {
                                DataGridViewRow tempRow = new DataGridViewRow();
                                DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                                cellSLNO.Value = intRow;
                                tempRow.Cells.Add(cellSLNO);

                                DataGridViewCell cellMainProductID = new DataGridViewTextBoxCell();
                                //cellMainProductID.Value = dt.Rows[i]["pm_product_id"];
                                cellMainProductID.Value = strArrProduct[0];
                                tempRow.Cells.Add(cellMainProductID);

                                DataGridViewCell cellMainProduct = new DataGridViewTextBoxCell();
                                //cellMainProduct.Value = dt.Rows[i]["pm_product_name"];
                                cellMainProduct.Value = tvProducts.Nodes[0].Nodes[i].Nodes[j].Text;//strArrProduct[3];
                                tempRow.Cells.Add(cellMainProduct);

                                DataGridViewCell cellSubProduct = new DataGridViewTextBoxCell();
                                //cellSubProduct.Value = dt.Rows[i]["category_name"];
                                cellSubProduct.Value = strArrProduct[3];
                                tempRow.Cells.Add(cellSubProduct);


                                //DataGridViewCell cellDessc = new DataGridViewTextBoxCell();
                                ////cellDessc.Value = dt.Rows[i]["pm_product_name"];
                                //cellDessc.Value = tvProducts.Nodes[0].Nodes[i].Nodes[j].Text;//strArrProduct[3];
                                //tempRow.Cells.Add(cellDessc);

                                DataGridViewCell cellQTY = new DataGridViewTextBoxCell();
                                cellQTY.Value = "1";
                                tempRow.Cells.Add(cellQTY);

                                DataGridViewCell cellRate = new DataGridViewTextBoxCell();

                                if (strArrProduct[5].ToString() == "009") //Combi packs
                                {
                                    //cellRate.Value = dt.Rows[i]["pm_bulk_mrp"];  
                                    cellRate.Value = Convert.ToDouble(strArrProduct[2]).ToString("f");
                                    tempRow.Cells.Add(cellRate);
                                }
                                else
                                {
                                    //cellRate.Value = dt.Rows[i]["pm_single_mrp"];
                                    cellRate.Value = Convert.ToDouble(strArrProduct[1]).ToString("f");
                                    tempRow.Cells.Add(cellRate);
                                }

                                if (strArrProduct[6] == "Free")
                                    cellRate.Value = "00.01";

                                DataGridViewCell cellPoints = new DataGridViewTextBoxCell();
                                //cellPoints.Value = dt.Rows[i]["TIPPoints"];
                                cellPoints.Value = Convert.ToDouble(strArrProduct[4]).ToString("f");
                                tempRow.Cells.Add(cellPoints);

                                intRow = intRow + 1;
                                dgvProducts.Rows.Add(tempRow);

                            }
                        }
                    }
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Select Product!", "Product Search");
            }
        }

        private void AddItemsToGridInvoice(DataGridView dgvProducts)
        {
            string[] strArrProduct = null;
            bool isItemExisted = false;
            int intRow = 0;

            intRow = dgvProducts.Rows.Count + 1;

            if (CheckData())
            {

                for (int i = 0; i < tvProducts.Nodes[0].Nodes.Count; i++)
                {
                    for (int j = 0; j < tvProducts.Nodes[0].Nodes[i].Nodes.Count; j++)
                    {
                        if (tvProducts.Nodes[0].Nodes[i].Nodes[j].Checked == true)
                        {
                            string strProduct = tvProducts.Nodes[0].Nodes[i].Nodes[j].Name.ToString();//((NewCheckboxListItem)(clbProduct.Items[i])).Tag;
                            strArrProduct = strProduct.Split('^');
                            for (int nRow = 0; nRow < dgvProducts.Rows.Count; nRow++)
                            {
                                if (dgvProducts.Rows[nRow].Cells["ProductID"].Value.ToString().Trim() == strArrProduct[0].ToString().Trim())
                                {
                                    isItemExisted = true;
                                    break;
                                }
                            }
                            if ((Convert.ToDouble(strArrProduct[1]) > 0 || (Convert.ToDouble(strArrProduct[1]) > 0)) && isItemExisted == false)  //dt.Rows[i]["pm_single_mrp"] or dt.Rows[i]["PM_BULK_MRP"]
                            {
                                DataGridViewRow tempRow = new DataGridViewRow();
                                DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                                cellSLNO.Value = intRow;
                                tempRow.Cells.Add(cellSLNO);

                                DataGridViewCell cellMainProductID = new DataGridViewTextBoxCell();
                                //cellMainProductID.Value = dt.Rows[i]["pm_product_id"];
                                cellMainProductID.Value = strArrProduct[0];
                                tempRow.Cells.Add(cellMainProductID);

                                DataGridViewCell cellMainProduct = new DataGridViewTextBoxCell();
                                //cellMainProduct.Value = dt.Rows[i]["pm_product_name"];
                                cellMainProduct.Value = tvProducts.Nodes[0].Nodes[i].Nodes[j].Text;//strArrProduct[3];
                                tempRow.Cells.Add(cellMainProduct);

                                DataGridViewCell cellSubProduct = new DataGridViewTextBoxCell();
                                //cellSubProduct.Value = dt.Rows[i]["category_name"];
                                cellSubProduct.Value = strArrProduct[3];
                                tempRow.Cells.Add(cellSubProduct);

                                DataGridViewCell cellDessc = new DataGridViewTextBoxCell();
                                //cellDessc.Value = dt.Rows[i]["pm_product_name"];
                                cellDessc.Value = tvProducts.Nodes[0].Nodes[i].Nodes[j].Text;//strArrProduct[3];
                                tempRow.Cells.Add(cellDessc);

                                DataGridViewCell cellQTY = new DataGridViewTextBoxCell();
                                cellQTY.Value = "";
                                tempRow.Cells.Add(cellQTY);

                                DataGridViewCell cellRate = new DataGridViewTextBoxCell();

                                if (strArrProduct[5].ToString() == "009") //Combi packs
                                {
                                    //cellRate.Value = dt.Rows[i]["pm_bulk_mrp"];  
                                    cellRate.Value = Convert.ToDouble(strArrProduct[2]).ToString("f");
                                    tempRow.Cells.Add(cellRate);
                                }
                                else
                                {
                                    //cellRate.Value = dt.Rows[i]["pm_single_mrp"];
                                    cellRate.Value = Convert.ToDouble(strArrProduct[1]).ToString("f");
                                    tempRow.Cells.Add(cellRate);
                                }

                                if (strArrProduct[6] == "Free")
                                    cellRate.Value = "00.01";

                                DataGridViewCell cellAmt = new DataGridViewTextBoxCell();
                                if (strArrProduct[6] == "Free")
                                    cellAmt.Value = "00.01";
                                else
                                    cellAmt.Value = "";

                                tempRow.Cells.Add(cellAmt);

                                DataGridViewCell cellPoints = new DataGridViewTextBoxCell();
                                //cellPoints.Value = dt.Rows[i]["TIPPoints"];
                                cellPoints.Value = Convert.ToDouble(strArrProduct[4]).ToString("f");
                                tempRow.Cells.Add(cellPoints);

                                DataGridViewCell cellDBRate = new DataGridViewTextBoxCell();
                                cellDBRate.Value = cellRate.Value;
                                tempRow.Cells.Add(cellDBRate);

                                DataGridViewCell cellDBPoints = new DataGridViewTextBoxCell();
                                cellDBPoints.Value = cellPoints.Value;
                                tempRow.Cells.Add(cellDBPoints);

                                if (strArrProduct[6] == "Combi")
                                    tempRow.Cells[2].Style.BackColor = Color.FromArgb(192, 192, 255);
                                else if (strArrProduct[6] == "Free")
                                    tempRow.Cells[2].Style.BackColor = Color.MediumTurquoise;
                                //tempRow.Cells[6].Style.Font.Bold = true;
                                intRow = intRow + 1;
                                dgvProducts.Rows.Add(tempRow);

                            }
                        }
                    }
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Select Product!", "Product Search");
            }
        }

        private void AddItemsToGridDoorKnocks(DataGridView dgvProducts)
        {
            string[] strArrProduct = null;
            bool isItemExisted = false;
            int intRow = 0;

            intRow = dgvProducts.Rows.Count + 1;

            if (CheckData())
            {

                for (int i = 0; i < tvProducts.Nodes[0].Nodes.Count; i++)
                {
                    for (int j = 0; j < tvProducts.Nodes[0].Nodes[i].Nodes.Count; j++)
                    {
                        if (tvProducts.Nodes[0].Nodes[i].Nodes[j].Checked == true)
                        {
                            string strProduct = tvProducts.Nodes[0].Nodes[i].Nodes[j].Name.ToString();//((NewCheckboxListItem)(clbProduct.Items[i])).Tag;
                            strArrProduct = strProduct.Split('^');
                            for (int nRow = 0; nRow < dgvProducts.Rows.Count; nRow++)
                            {
                                if (dgvProducts.Rows[nRow].Cells["ProductID"].Value.ToString().Trim() == strArrProduct[0].ToString().Trim())
                                {
                                    isItemExisted = true;
                                    break;
                                }
                            }
                            if ((Convert.ToDouble(strArrProduct[1]) > 0 || (Convert.ToDouble(strArrProduct[1]) > 0)) && isItemExisted == false)  //dt.Rows[i]["pm_single_mrp"] or dt.Rows[i]["PM_BULK_MRP"]
                            {
                                DataGridViewRow tempRow = new DataGridViewRow();
                                DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                                cellSLNO.Value = intRow;
                                tempRow.Cells.Add(cellSLNO);

                                DataGridViewCell cellMainProductID = new DataGridViewTextBoxCell();
                                //cellMainProductID.Value = dt.Rows[i]["pm_product_id"];
                                cellMainProductID.Value = strArrProduct[0];
                                tempRow.Cells.Add(cellMainProductID);

                                DataGridViewCell cellMainProduct = new DataGridViewTextBoxCell();
                                //cellMainProduct.Value = dt.Rows[i]["pm_product_name"];
                                cellMainProduct.Value = tvProducts.Nodes[0].Nodes[i].Nodes[j].Text;//strArrProduct[3];
                                tempRow.Cells.Add(cellMainProduct);

                                DataGridViewCell cellMainBrand = new DataGridViewTextBoxCell();
                                //cellMainProduct.Value = dt.Rows[i]["category_name"];
                                cellMainBrand.Value = strArrProduct[3];
                                tempRow.Cells.Add(cellMainBrand);

                                DataGridViewCell cellPoints = new DataGridViewTextBoxCell();
                                cellPoints.Value = Convert.ToDouble(strArrProduct[4]).ToString("f");
                                tempRow.Cells.Add(cellPoints);

                                intRow = intRow + 1;
                                dgvProducts.Rows.Add(tempRow);

                            }
                        }
                    }
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Select Product!", "Product Search");
            }
        }

        private void AddFreeItemsToGrid(DataGridView dgvProducts)
        {
            string[] strArrProduct = null;
            bool isItemExisted = false;
            int intRow = 0;

            intRow = dgvProducts.Rows.Count + 1;

            if (CheckData())
            {

                for (int i = 0; i < tvProducts.Nodes[0].Nodes.Count; i++)
                {
                    for (int j = 0; j < tvProducts.Nodes[0].Nodes[i].Nodes.Count; j++)
                    {
                        if (tvProducts.Nodes[0].Nodes[i].Nodes[j].Checked == true)
                        {
                            string strProduct = tvProducts.Nodes[0].Nodes[i].Nodes[j].Name.ToString();//((NewCheckboxListItem)(clbProduct.Items[i])).Tag;
                            strArrProduct = strProduct.Split('^');
                            for (int nRow = 0; nRow < dgvProducts.Rows.Count; nRow++)
                            {
                                if (dgvProducts.Rows[nRow].Cells["ProductID"].Value.ToString().Trim() == strArrProduct[0].ToString().Trim())
                                {
                                    isItemExisted = true;
                                    break;
                                }
                            }
                            if ((Convert.ToDouble(strArrProduct[1]) > 0 || (Convert.ToDouble(strArrProduct[1]) > 0)) && isItemExisted == false)  //dt.Rows[i]["pm_single_mrp"] or dt.Rows[i]["PM_BULK_MRP"]
                            {
                                DataGridViewRow tempRow = new DataGridViewRow();
                                DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                                cellSLNO.Value = intRow;
                                tempRow.Cells.Add(cellSLNO);

                                DataGridViewCell cellMainProductID = new DataGridViewTextBoxCell();
                                //cellMainProductID.Value = dt.Rows[i]["pm_product_id"];
                                cellMainProductID.Value = strArrProduct[0];
                                tempRow.Cells.Add(cellMainProductID);

                                DataGridViewCell cellMainProduct = new DataGridViewTextBoxCell();
                                //cellMainProduct.Value = dt.Rows[i]["pm_product_name"];
                                cellMainProduct.Value = tvProducts.Nodes[0].Nodes[i].Nodes[j].Text;//strArrProduct[3];
                                tempRow.Cells.Add(cellMainProduct);

                                DataGridViewCell cellMainBrand = new DataGridViewTextBoxCell();
                                //cellMainProduct.Value = dt.Rows[i]["category_name"];
                                cellMainBrand.Value = strArrProduct[3];
                                tempRow.Cells.Add(cellMainBrand);

                                DataGridViewCell cellPoints = new DataGridViewTextBoxCell();
                                cellPoints.Value = Convert.ToDouble(strArrProduct[4]).ToString("f");
                                tempRow.Cells.Add(cellPoints);

                                intRow = intRow + 1;
                                dgvProducts.Rows.Add(tempRow);

                            }
                        }
                    }
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Select Product!", "Product Search");
            }
        }

        private void AddItemsToGridSalesOrders(DataGridView dgvProducts)
        {
            string[] strArrProduct = null;
            bool isItemExisted = false;
            int intRow = 0;

            intRow = dgvProducts.Rows.Count + 1;

            if (CheckData())
            {

                for (int i = 0; i < tvProducts.Nodes[0].Nodes.Count; i++)
                {
                    for (int j = 0; j < tvProducts.Nodes[0].Nodes[i].Nodes.Count; j++)
                    {
                        if (tvProducts.Nodes[0].Nodes[i].Nodes[j].Checked == true)
                        {
                            string strProduct = tvProducts.Nodes[0].Nodes[i].Nodes[j].Name.ToString();//((NewCheckboxListItem)(clbProduct.Items[i])).Tag;
                            strArrProduct = strProduct.Split('^');
                            for (int nRow = 0; nRow < dgvProducts.Rows.Count; nRow++)
                            {
                                if (dgvProducts.Rows[nRow].Cells["ProductID"].Value.ToString().Trim() == strArrProduct[0].ToString().Trim())
                                {
                                    isItemExisted = true;
                                    break;
                                }
                            }
                            if ((Convert.ToDouble(strArrProduct[1]) > 0 || (Convert.ToDouble(strArrProduct[1]) > 0)) && isItemExisted == false)  //dt.Rows[i]["pm_single_mrp"] or dt.Rows[i]["PM_BULK_MRP"]
                            {
                                DataGridViewRow tempRow = new DataGridViewRow();
                                DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                                cellSLNO.Value = intRow;
                                tempRow.Cells.Add(cellSLNO);

                                DataGridViewCell cellMainProductID = new DataGridViewTextBoxCell();
                                //cellMainProductID.Value = dt.Rows[i]["pm_product_id"];
                                cellMainProductID.Value = strArrProduct[0];
                                tempRow.Cells.Add(cellMainProductID);

                                DataGridViewCell cellMainProduct = new DataGridViewTextBoxCell();
                                //cellMainProduct.Value = dt.Rows[i]["pm_product_name"];
                                cellMainProduct.Value = tvProducts.Nodes[0].Nodes[i].Nodes[j].Text;//strArrProduct[3];
                                tempRow.Cells.Add(cellMainProduct);

                                DataGridViewCell cellMainBrand = new DataGridViewTextBoxCell();
                                //cellMainProduct.Value = dt.Rows[i]["category_name"];
                                cellMainBrand.Value = strArrProduct[3];
                                tempRow.Cells.Add(cellMainBrand);

                                DataGridViewCell cellQTY = new DataGridViewTextBoxCell();
                                cellQTY.Value = "";
                                tempRow.Cells.Add(cellQTY);

                                DataGridViewCell cellRate = new DataGridViewTextBoxCell();

                                if (strArrProduct[5].ToString() == "009") //Combi packs
                                {
                                    //cellRate.Value = dt.Rows[i]["pm_bulk_mrp"];  
                                    cellRate.Value = Convert.ToDouble(strArrProduct[2]).ToString("f");
                                    tempRow.Cells.Add(cellRate);
                                }
                                else
                                {
                                    //cellRate.Value = dt.Rows[i]["pm_single_mrp"];
                                    cellRate.Value = Convert.ToDouble(strArrProduct[1]).ToString("f");
                                    tempRow.Cells.Add(cellRate);
                                }

                                if (strArrProduct[6] == "Free")
                                    cellRate.Value = "00.01";



                                DataGridViewCell cellAmt = new DataGridViewTextBoxCell();
                                cellAmt.Value = "";
                                tempRow.Cells.Add(cellAmt);


                                intRow = intRow + 1;
                                dgvProducts.Rows.Add(tempRow);

                            }
                        }
                    }
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Select Product!", "Product Search");
            }
        }

        private void AddItemsToGridSaleProducts(DataGridView dgvProducts)
        {
            string[] strArrProduct = null;
            bool isItemExisted = false;
            int intRow = 0;

            intRow = dgvProducts.Rows.Count + 1;

            if (CheckData())
            {

                for (int i = 0; i < tvProducts.Nodes[0].Nodes.Count; i++)
                {
                    for (int j = 0; j < tvProducts.Nodes[0].Nodes[i].Nodes.Count; j++)
                    {
                        if (tvProducts.Nodes[0].Nodes[i].Nodes[j].Checked == true)
                        {
                            string strProduct = tvProducts.Nodes[0].Nodes[i].Nodes[j].Name.ToString();//((NewCheckboxListItem)(clbProduct.Items[i])).Tag;
                            strArrProduct = strProduct.Split('^');
                            for (int nRow = 0; nRow < dgvProducts.Rows.Count; nRow++)
                            {
                                if (dgvProducts.Rows[nRow].Cells["ProductID"].Value.ToString().Trim() == strArrProduct[0].ToString().Trim())
                                {
                                    isItemExisted = true;
                                    break;
                                }
                            }
                            if ((Convert.ToDouble(strArrProduct[1]) > 0 || (Convert.ToDouble(strArrProduct[1]) > 0)) && isItemExisted == false)  //dt.Rows[i]["pm_single_mrp"] or dt.Rows[i]["PM_BULK_MRP"]
                            {
                                DataGridViewRow tempRow = new DataGridViewRow();
                                DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                                cellSLNO.Value = intRow;
                                tempRow.Cells.Add(cellSLNO);

                                DataGridViewCell cellMainProductID = new DataGridViewTextBoxCell();
                                //cellMainProductID.Value = dt.Rows[i]["pm_product_id"];
                                cellMainProductID.Value = strArrProduct[0];
                                tempRow.Cells.Add(cellMainProductID);

                                DataGridViewCell cellMainProduct = new DataGridViewTextBoxCell();
                                //cellMainProduct.Value = dt.Rows[i]["pm_product_name"];
                                cellMainProduct.Value = tvProducts.Nodes[0].Nodes[i].Nodes[j].Text;//strArrProduct[3];
                                tempRow.Cells.Add(cellMainProduct);

                                DataGridViewCell cellMainBrand = new DataGridViewTextBoxCell();
                                //cellMainProduct.Value = dt.Rows[i]["category_name"];
                                cellMainBrand.Value = strArrProduct[3];
                                tempRow.Cells.Add(cellMainBrand);

                                DataGridViewCell cellQTY = new DataGridViewTextBoxCell();
                                cellQTY.Value = "";
                                tempRow.Cells.Add(cellQTY);

                                intRow = intRow + 1;
                                dgvProducts.Rows.Add(tempRow);

                            }
                        }
                    }
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Select Product!", "Product Search");
            }
        }

        private void AddItemsToGridFreeProducts(DataGridView dgvProducts)
        {
            string[] strArrProduct = null;
            bool isItemExisted = false;
            int intRow = 0;

            intRow = dgvProducts.Rows.Count + 1;

            if (CheckData())
            {

                for (int i = 0; i < tvProducts.Nodes[0].Nodes.Count; i++)
                {
                    for (int j = 0; j < tvProducts.Nodes[0].Nodes[i].Nodes.Count; j++)
                    {
                        if (tvProducts.Nodes[0].Nodes[i].Nodes[j].Checked == true)
                        {
                            string strProduct = tvProducts.Nodes[0].Nodes[i].Nodes[j].Name.ToString();
                            strArrProduct = strProduct.Split('^');
                            for (int nRow = 0; nRow < dgvProducts.Rows.Count; nRow++)
                            {
                                if (dgvProducts.Rows[nRow].Cells["fProductID"].Value.ToString().Trim() == strArrProduct[0].ToString().Trim())
                                {
                                    isItemExisted = true;
                                    break;
                                }
                            }
                            if ((Convert.ToDouble(strArrProduct[1]) > 0 || (Convert.ToDouble(strArrProduct[1]) > 0)) && isItemExisted == false)
                            {
                                DataGridViewRow tempRow = new DataGridViewRow();
                                DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                                cellSLNO.Value = intRow;
                                tempRow.Cells.Add(cellSLNO);

                                DataGridViewCell cellMainProductID = new DataGridViewTextBoxCell();
                                cellMainProductID.Value = strArrProduct[0];
                                tempRow.Cells.Add(cellMainProductID);

                                DataGridViewCell cellMainProduct = new DataGridViewTextBoxCell();
                                cellMainProduct.Value = tvProducts.Nodes[0].Nodes[i].Nodes[j].Text;
                                tempRow.Cells.Add(cellMainProduct);

                                DataGridViewCell cellMainBrand = new DataGridViewTextBoxCell();
                                cellMainBrand.Value = strArrProduct[3];
                                tempRow.Cells.Add(cellMainBrand);

                                DataGridViewCell cellQTY = new DataGridViewTextBoxCell();
                                cellQTY.Value = "";
                                tempRow.Cells.Add(cellQTY);

                                intRow = intRow + 1;
                                dgvProducts.Rows.Add(tempRow);

                            }
                        }
                    }
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Select Product!", "Product Search");
            }
        }

        private void AddItemsToGridSPRefillProducts(DataGridView dgvProducts)
        {
            string[] strArrProduct = null;
            bool isItemExisted = false;
            int intRow = 0;

            intRow = dgvProducts.Rows.Count + 1;
            if (CheckData())
            {
                for (int i = 0; i < tvProducts.Nodes[0].Nodes.Count; i++)
                {
                    for (int j = 0; j < tvProducts.Nodes[0].Nodes[i].Nodes.Count; j++)
                    {
                        if (tvProducts.Nodes[0].Nodes[i].Nodes[j].Checked == true)
                        {
                            string strProduct = tvProducts.Nodes[0].Nodes[i].Nodes[j].Name.ToString();
                            strArrProduct = strProduct.Split('^');
                            for (int nRow = 0; nRow < dgvProducts.Rows.Count; nRow++)
                            {
                                if (strInvType == "SPRefill_dist")
                                {
                                    if (dgvProducts.Rows[nRow].Cells["DistProductID"].Value.ToString().Trim() == strArrProduct[0].ToString().Trim())
                                    {
                                        isItemExisted = true;
                                        break;
                                    }
                                }
                                else
                                {
                                    if (dgvProducts.Rows[nRow].Cells["ProductID"].Value.ToString().Trim() == strArrProduct[0].ToString().Trim())
                                    {
                                        isItemExisted = true;
                                        break;
                                    }
                                }
                            }
                            if ((Convert.ToDouble(strArrProduct[1]) > 0 || (Convert.ToDouble(strArrProduct[1]) > 0)) && isItemExisted == false)
                            {
                                DataGridViewRow tempRow = new DataGridViewRow();
                                DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                                cellSLNO.Value = intRow;
                                tempRow.Cells.Add(cellSLNO);

                                DataGridViewCell cellMainProductID = new DataGridViewTextBoxCell();
                                cellMainProductID.Value = strArrProduct[0];
                                tempRow.Cells.Add(cellMainProductID);

                                DataGridViewCell cellMainProduct = new DataGridViewTextBoxCell();
                                cellMainProduct.Value = tvProducts.Nodes[0].Nodes[i].Nodes[j].Text;
                                tempRow.Cells.Add(cellMainProduct);

                                DataGridViewCell cellMainBrand = new DataGridViewTextBoxCell();
                                cellMainBrand.Value = strArrProduct[3];
                                tempRow.Cells.Add(cellMainBrand);

                                DataGridViewCell cellUomQty = new DataGridViewTextBoxCell();
                                cellUomQty.Value = strArrProduct[7];
                                tempRow.Cells.Add(cellUomQty);

                                DataGridViewCell cellQTY = new DataGridViewTextBoxCell();
                                cellQTY.Value = "";
                                tempRow.Cells.Add(cellQTY);

                                DataGridViewCell cellDBUomQty = new DataGridViewTextBoxCell();
                                cellDBUomQty.Value = strArrProduct[7];
                                tempRow.Cells.Add(cellDBUomQty);

                                DataGridViewCell cellDBUomType = new DataGridViewTextBoxCell();
                                cellDBUomType.Value = strArrProduct[8];
                                tempRow.Cells.Add(cellDBUomType);

                                intRow = intRow + 1;
                                dgvProducts.Rows.Add(tempRow);

                                if (strInvType == "SPRefill_dist")
                                {
                                    if (dgvProducts.Rows.Count > 1)
                                    {
                                        MessageBox.Show("Please Select One Record Only.", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        dgvProducts.Rows.Clear();
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Select Product!", "Product Search");
            }
        }

        private void AddItemsToGridInternalDamage(DataGridView dgvProducts)
        {
            string[] strArrProduct = null;
            bool isItemExisted = false;
            int intRow = 0;

            intRow = dgvProducts.Rows.Count + 1;

            if (CheckData())
            {

                for (int i = 0; i < tvProducts.Nodes[0].Nodes.Count; i++)
                {
                    for (int j = 0; j < tvProducts.Nodes[0].Nodes[i].Nodes.Count; j++)
                    {
                        if (tvProducts.Nodes[0].Nodes[i].Nodes[j].Checked == true)
                        {
                            string strProduct = tvProducts.Nodes[0].Nodes[i].Nodes[j].Name.ToString();
                            strArrProduct = strProduct.Split('^');
                            for (int nRow = 0; nRow < dgvProducts.Rows.Count; nRow++)
                            {
                                if (dgvProducts.Rows[nRow].Cells["ProductID"].Value.ToString().Trim() == strArrProduct[0].ToString().Trim())
                                {
                                    isItemExisted = true;
                                    break;
                                }
                            }
                            if ((Convert.ToDouble(strArrProduct[1]) > 0 || (Convert.ToDouble(strArrProduct[1]) > 0)) && isItemExisted == false)
                            {
                                DataGridViewRow tempRow = new DataGridViewRow();
                                DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                                cellSLNO.Value = intRow;
                                tempRow.Cells.Add(cellSLNO);

                                DataGridViewCell cellMainProductID = new DataGridViewTextBoxCell();
                                cellMainProductID.Value = strArrProduct[0];
                                tempRow.Cells.Add(cellMainProductID);

                                DataGridViewCell cellMainProduct = new DataGridViewTextBoxCell();
                                cellMainProduct.Value = tvProducts.Nodes[0].Nodes[i].Nodes[j].Text;
                                tempRow.Cells.Add(cellMainProduct);

                                DataGridViewCell cellMainBrand = new DataGridViewTextBoxCell();
                                cellMainBrand.Value = strArrProduct[3];
                                tempRow.Cells.Add(cellMainBrand);

                                DataGridViewCell cellBatch = new DataGridViewTextBoxCell();
                                cellBatch.Value = "GENERAL";
                                tempRow.Cells.Add(cellBatch);

                                DataGridViewCell cellQTY = new DataGridViewTextBoxCell();
                                cellQTY.Value = "";
                                tempRow.Cells.Add(cellQTY);

                                intRow = intRow + 1;
                                dgvProducts.Rows.Add(tempRow);
                            }
                        }
                    }
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Select Product!", "Product Search");
            }
        }

        private bool CheckData()
        {
            bool blVil = false;

            for (int i = 0; i < tvProducts.Nodes[0].Nodes.Count; i++)
            {
                for (int j = 0; j < tvProducts.Nodes[0].Nodes[i].Nodes.Count; j++)
                {
                    if (tvProducts.Nodes[0].Nodes[i].Nodes[j].Checked == true)
                    {
                        blVil = true;
                        break;
                    }
                }
                if (blVil == true)
                    break;
            }
            //for (int i = 0; i < clbProduct.Items.Count; i++)
            //{
            //    if (clbProduct.GetItemCheckState(i) == CheckState.Checked)
            //    {
            //        blVil = true;
            //    }
            //}
            return blVil;
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {

            string SearchText = this.txtSearch.Text;
            if (SearchText == "")
            {
                return;
            };
            TreeNode SelectedNode = SearchNode(SearchText, tvProducts.Nodes[0]);
            if (SelectedNode != null)
            {
                this.tvProducts.SelectedNode = SelectedNode;
                this.tvProducts.SelectedNode.Expand();
                this.tvProducts.Select();
                //txtSearch.Focus();
            };


        }

        private void tvProducts_AfterCheck(object sender, TreeViewEventArgs e)
        {
            tvProducts.BeginUpdate();

            foreach (TreeNode Node in e.Node.Nodes)
            {
                Node.Checked = e.Node.Checked;
            }

            tvProducts.EndUpdate();

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }


    }
}