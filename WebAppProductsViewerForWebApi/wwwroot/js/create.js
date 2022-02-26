const uri="https://localhost:5001/products";
const uriRedirect="https://localhost:5021";


async function prikazData(data) {
  /*var dataArr = data["data"]; varijanta kad se spakuju podaci u JSON */
  let nameOfArticleFeature = document.querySelector("#create-name");
  let priceOfArticleFeature = document.querySelector("#create-price");
  let colorOfArticleFeature = document.querySelector("#create-color");
  let quantityOfArticleFeature = document.querySelector("#create-quantity");
  let idOfArticleFeature = document.querySelector("#create-id");
  let sizeOfArticleFeature = document.querySelector("#create-size");
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

function createItem() {
 
  var uriCreate = uri + "/Create";
 
  const item = {    
    name: document.getElementById("create-name").value.trim(),
    color: document.getElementById("create-color").value.trim(),
    size: document.getElementById("create-size").value.trim(),
    price: document.getElementById("create-price").value.trim(),
    quantity: document.getElementById("create-quantity").value.trim(),
  };

  fetch(`${uriCreate}`, {
    method: "POST",
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json",
    },
    body: JSON.stringify(item),
  })
    .then((data) => {
        console.log(data);
        prikazData(data);
        window.location.replace(uriRedirect);
    })
    .catch((error) => console.error("Unable to create item.", error));

  closeInput();

  return false;
}

function closeInput() {
    document.getElementById('createForm').style.display = 'none';
  }
