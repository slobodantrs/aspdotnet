$(document).ready(function () {
    
     onStartLoad();
   
    
   });
   let table;
   async function onStartLoad() {
    //call getData function
    getData()
      .then((data) => {    
        loadDataList(data);        
      })
      .catch((error) => {
        alert("Unable to get items." + error);
      }); 
      
    const idFeature=3;
    getFeatureData(idFeature)
    .then((data) => {
      prikazFeatureData(data);
      
      //console.log(data["data"].toString());
    })
    .catch((error) => {
      alert("Unable to get items." + error);
    }); //log the data
  
  }

  async function getData() {
    //await the response of the fetch call
    let response = await fetch("https://localhost:5001/products");
    //proceed once the first promise is resolved.
    let data = await response.json();
    //proceed only when the second promise is resolved
    return data;
  }
  async function loadDataList(data) {
    let product_list = document.querySelector("#productList");
    table = document.createElement("table");
    table.classList.add("productsTable");
    product_list.appendChild(table);
    let i = 0;
    let row;
    for (key in data) {
      var obj = data[key];
      if (obj instanceof Object) {
        if (i % 4 == 0) {
          row = document.createElement("TR");
          row.classList.add("d-flex");
          row.classList.add("flex-row");
          row.classList.add("justify-content-around");
          //It has 4 div elements in a row
          table.appendChild(row); //Add row in the table
        }
  
        let h1Title = document.createElement("h2");
        let priceParagraph = document.createElement("p");
        let colorParagraph = document.createElement("p");
        let sizeParagraph = document.createElement("p");
        let quantityParagraph = document.createElement("p");

        /*New*/ 
        let btnDiv=document.createElement("div");
        let aEdit=document.createElement("a");
        let aDelete=document.createElement("a");
       
        btnDiv.classList.add("d-flex");
        aEdit.classList.add("btn");
        aEdit.classList.add("btn-success");
        aEdit.classList.add("text-white");
        aEdit.setAttribute('href','edit.html?id='+obj.productId);
        aEdit.style.width = '100%';
        aEdit.innerText = 'Edit';

        aDelete.classList.add("btn");
      aDelete.classList.add("btn-danger");
      aDelete.classList.add("text-white");        
      aDelete.style.width = '100%';
      aDelete.style.cursor = 'pointer';
      aDelete.innerText = 'Delete';
      const urlDel='https://localhost:5001/products/Delete/'+obj.productId;
      aDelete.onclick= function(){
        Delete(urlDel);
      };
   
       
     
       
        h1Title.innerHTML = h1Title.innerHTML + obj.name;
        priceParagraph.innerHTML =
          priceParagraph.innerHTML + "Price: " + obj.price + " $";
        colorParagraph.innerHTML =
          colorParagraph.innerHTML + "Color: " + obj.color;
        sizeParagraph.innerHTML = sizeParagraph.innerHTML + "Size: " + obj.size;
        quantityParagraph.innerHTML =
          quantityParagraph.innerHTML + "Quantity: " + obj.quantity;
  
        let column = document.createElement("TD");
        column.appendChild(h1Title);
        column.appendChild(priceParagraph);
        column.appendChild(colorParagraph);
        column.appendChild(sizeParagraph);
        column.appendChild(quantityParagraph);  
        column.appendChild(btnDiv);/*new*/ 
        btnDiv.appendChild(aEdit);/*new*/ 
        btnDiv.appendChild(aDelete);/*new*/       
     
        row.appendChild(column);
  
        i++;
      }
    }
  }

  async function getFeatureData(id) {
    //await the response of the fetch call
    let response = await fetch("https://localhost:5001/products/"+id);
    //proceed once the first promise is resolved.
    let data = await response.json();
    //proceed only when the second promise is resolved
    return data;
  }

  async function prikazFeatureData(data) {
    /*var dataArr = data["data"]; varijanta kad se spakuju podaci u JSON */
    let nameOfArticleFeature = document.querySelector("#prod1");
    let priceOfArticleFeature = document.querySelector("#prodPrice");
    nameOfArticleFeature.innerHTML = data.name;
    priceOfArticleFeature.innerHTML = data.price + " $";
     
  }

  /*New*/ 
  async function Delete(url) {
    await swal({
      title: "Are you sure?",
      text: "You will not be able to recover this imaginary file!",
      icon: "warning",
      buttons: true,
      dangerMode: true,
    }).then((willDelete) => {
      if (willDelete) {
        $.ajax({
          url: url,
          dataSrc: function (data) {
            var json = JSON.parse(JSON.stringify(data));
            console.log(json);
            return json;
          },
          type: "DELETE",
          datatype: "json",
  
          success: function (data) {
            if (data.success) {
              toastr.success(data.success);
              window.location.reload();
              //table.load(location.href + " .productsTable");
            } else {
              toastr.err(data.message);
            }
          },
        });
      } else {
      }
    });
  }

  
    