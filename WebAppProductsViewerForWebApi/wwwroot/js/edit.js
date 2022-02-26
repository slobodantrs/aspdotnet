$(document).ready(function () {
    onStartLoad();
  });

  const uri="https://localhost:5001/products";
  const uriRedirect="https://localhost:5021";

  async function onStartLoad() {
    var baseUrl = window.location.href; // You can also use document.URL
    var id = baseUrl.substring(baseUrl.lastIndexOf("=") + 1);
  
   // alert(id); // Alert last segment
    //call getData function
    getData(id).then((data) => {
      prikazData(data);
      //console.log(data["data"].toString());
    }); //log the data
  }


async function getData(id) {
    //await the response of the fetch call
    let response = await fetch("https://localhost:5001/products/" + id);
    //proceed once the first promise is resolved.
    let data = await response.json();
    //proceed only when the second promise is resolved
    return data;
  }
  
  async function prikazData(data) {
    /*var dataArr = data["data"]; varijanta kad se spakuju podaci u JSON */
    let nameOfArticleFeature = document.querySelector("#edit-name");
    let priceOfArticleFeature = document.querySelector("#edit-price");
    let colorOfArticleFeature = document.querySelector("#edit-color");
    let quantityOfArticleFeature = document.querySelector("#edit-quantity");
    let idOfArticleFeature = document.querySelector("#edit-id");
    let sizeOfArticleFeature = document.querySelector("#edit-size");
    nameOfArticleFeature.innerHTML = data.name;
    nameOfArticleFeature.value = data.name;
    priceOfArticleFeature.innerHTML = data.price;
    priceOfArticleFeature.value = data.price;
    colorOfArticleFeature.innerHTML = data.color;
    colorOfArticleFeature.value = data.color;
    quantityOfArticleFeature.innerHTML = data.quantity;
    quantityOfArticleFeature.value = data.quantity;
    idOfArticleFeature.innerHTML = data.productId;
    idOfArticleFeature.value=data.productId;
    sizeOfArticleFeature.innerHTML = data.size;
    sizeOfArticleFeature.value = data.size;
  }

  function updateItem() {
 
    var uriUpdate = uri + "/Update";
    const itemId = document.getElementById("edit-id").value;
    const item = {
      productId: parseInt(itemId, 10),
  
      name: document.getElementById("edit-name").value.trim(),
      color: document.getElementById("edit-color").value.trim(),
      size: document.getElementById("edit-size").value.trim(),
      price: document.getElementById("edit-price").value.trim(),
      quantity: document.getElementById("edit-quantity").value.trim(),
    };
  
    fetch(`${uriUpdate}/${itemId}`, {
      method: "PUT",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      body: JSON.stringify(item),
    })
      .then(getData(itemId).then((data) => {
          prikazData(data);
          window.location.replace(uriRedirect);
      }))
      .catch((error) => console.error("Unable to update item.", error));
  
    //closeInput();
  
    return false;
  }