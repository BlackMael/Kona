<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<DetailsViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

<title>Kona: <%=Model.SelectedProduct.Name %></title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <!--Top Navigation-->
    <div id="hdr3">
    <ul class="topNav" id="menulist">
          <%foreach (Category c in Model.ParentCategories) { %>
            <li class="first"><a href="<%=Url.Action("Index",
            "Product",new {id=c.ID}) %>" 
            title="<%=c.Description %>"><%=c.Name %></a></li>
          <%} %>

    </ul>
    </div>
<div id="bdy" class="leftNav">
    <div class="colleft fltleft">
   
        <div class="leftnavMod">
            <h6>Favorites</h6>
            <ul class="leftnavList">
            <%foreach (var item in Model.Favorite){%>
                <li><a href="#"><%=item.Name %></a></li>
            <%}%>
            </ul>
        </div>

        <div class="leftnavMod">
            <h6>Recent</h6>
            <ul class="leftnavList">
            <%foreach (var item in Model.Recent){%>
                <li><a href="<%=Url.Action("Details","Product",new {sku=item.SKU}) %>"><%=item.Name %></a></li>
            <%}%>
            </ul>
        </div>
    </div>

    <div class="colmiddle fltleft">
    <!--Product widget with 1 product-->
    <div class="prodWidget">
        <h4>
            <%=Model.SelectedProduct.Name %></h4>
        <hr />
        <div class="fltleft">
            <img src="/content/productimages/<%=Model.SelectedProduct.DefaultImage.Replace("Thumbnail","Full") %>" alt=" <%=Model.SelectedProduct.Name %>" width="240" height="240" />
        </div>
        <div class="fltleft">
            <h6>
                Price:  <%=Model.SelectedProduct.Price.ToString("C") %></h6>
                <p style="text-align:center">
                    <div class="product">
                        <input type="hidden" class="product-title" value="<%=Model.SelectedProduct.Name %>">
                        <input type="hidden" class="product-price" value="<%=Model.SelectedProduct.Price.ToString("C") %>">
                        <div class="googlecart-add-button" tabindex="0" role="button" title="Add to cart">
                        </div>
                    </div>
                </p>
                </div>
            <div class="clearLayout">
            <%foreach (Descriptor d in Model.SelectedProduct.Descriptors) { %>
            
            <h6><%=d.Title%>:</h6>
            <p><%=d.Body%></p>
            
            <%} %>
            <div class="prodWidget">
            <h2 style="margin-top:20px;margin-bottom:20px;">You might also like...</h2>

            
            <%foreach (Product p in Model.Recommended) {
                  string desc = p.Descriptors.First().Body;
                  %>
            <h4><%=p.Name %></h4>
            <hr />
            <div class="fltleft prodItem">
                    <a href="<%=Url.Action("Details","Product",new {id=p.SKU}) %>">
                    <img src="/content/productimages/<%=p.DefaultImage %>" alt="<%=p.Name %>" width="115" height="115" />

            </div>
            <div class="fltright prodText">
                <p><%=desc.Substring(0,200)%></p>
                                    
                    <p style="text-align:center">
                        <div class="product">
                            <input type="hidden" class="product-title" value="<%=p.Name %>">
                            <input type="hidden" class="product-price" value="<%=p.Price.ToString("C") %>">
                            <div class="googlecart-add-button" tabindex="0" role="button" title="Add to cart">
                            </div>
                        </div>
                    </p>
            </div>
            <div class="clearLayout">
            </div>               
            <%} %>
            </div>
        </div>
    </div>
</div>

    <script  id='googlecart-script' 
    type='text/javascript' 
    src='https://checkout.google.com/seller/gsc/v2_2/cart.js?mid=653000314697240' 
    integration='jscart-wizard' 
    post-cart-to-sandbox='true' 
    currency='USD' 
    hide-cart-when-empty="true"
    productWeightUnits='LB'>
    </script>

</asp:Content>
