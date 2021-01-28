import { Restaurant     } from "./restaurant.js";
import { Cart           } from "./cart.js";
import { CreateOption   } from "./helpers.js";
import { CartItem       } from "./cartItem.js";

/*
    TODO:
    ---------------------------------------------------------------
    - Malo kod da se sredi;
    - Slika?;
    - Checkout;
    ---------------------------------------------------------------
*/


/* CART */
let cartButton                  = document.querySelector(".cart-button");
let sideCartPanel               = document.querySelector(".side");
let checkoutButton              = document.querySelector(".cart-checkout button");
const cart                      = new Cart();

/* RESTAURANT */        
let restaurantName              = document.querySelector(".main-content .main .title h2");
let restaurantAddr              = document.querySelector(".main-content .main .title p");
let restaurantSelect            = document.querySelector(".restaurant-select");
const restaurantData            = [];
let currentRestaurantIndex      = 0;

/* PRODUCT MANAGEMENT */
let form                        = document.querySelector(".product-management .form-container form");
let productManagementPanel      = document.querySelector(".product-management"); 
let productManagementOpenBtn    = document.querySelector(".mng-open-button");
let productManagementCloseBtn   = document.querySelector(".product-management .close-button");
let productSubmitButton         = document.querySelector(".submit-button");
let productDeleteButton         = document.querySelector(".delete-button");
let productSelect               = document.querySelector(".product-select");
let errorMessage                = document.querySelector(".product-management .error-message");
let operationRadios             = document.querySelectorAll('input[type=radio][name="operation"]');
let isUpdate                    = false;



fetch(`https://localhost:5001/Restaurant/GetRestaurants`, {
    headers: { "Content-Type": "application/json; charset=UTF-8" }
}).then(p => {
    if (p.ok)
    {
        p.json().then( (data) => {
            let count = 0;
            data.forEach(d => {
                let r = new Restaurant(d);
                
                d.products.forEach(x => r.Add(x));

                restaurantData.push(r);
                restaurantSelect.appendChild(CreateOption(d.name, count++));
            });
            LoadRestaurant();
        });
    }
}).catch(x => console.log(x));


fetch(`https://localhost:5001/Restaurant/GetCartItems`, {
    headers: { "Content-Type": "application/json; charset=UTF-8" }
}).then(p => {
    if (p.ok)
    {
        p.json().then((data) => {
          
          data.forEach(d => {
                d.product.ID = d.product.id;
                let o = new CartItem(d.product, d.quantity);
                cart.AddLocal(o);
            });
        });
    }
}).catch(x => console.log(x));

restaurantSelect.addEventListener('change', () => LoadRestaurant(restaurantSelect.value));

let LoadRestaurant = (index = 0) => {
    currentRestaurantIndex = index;
    
    restaurantName.innerHTML = restaurantData[index].ime;
    restaurantAddr.innerHTML = restaurantData[index].address;
    document.styleSheets[1].addRule('.title h2::after','content: " ' + restaurantData[index].phoneNumber + '";');
    
    restaurantData[index].Draw();


    
    operationRadios[0].checked = true;
    form.querySelector("#product-input").style.display      = 'none';
    form.querySelector("#name-input").style.display         = 'flex';
    form.querySelector("#description-input").style.display  = 'flex';
    form.querySelector("#price-input").style.display        = 'flex';
    
    FillForm(false);
    UpdateDropDownMenu();
};

let FillForm = (x) => {
    let nameInput        = form.querySelector("#name-input input");    
    let descriptionInput = form.querySelector("#description-input textarea");
    let priceInput       = form.querySelector("#price-input input");
    
    const product = restaurantData[currentRestaurantIndex].products.find(x => x.ID == productSelect.value);
    
    nameInput.value        = (!x && product) ? product.name         : "";
    descriptionInput.value = (!x && product) ? product.description  : "";
    priceInput.value       = (!x && product) ? product.price        : "";
}

let UpdateDropDownMenu = () => {
    productSelect.innerHTML = "";

    restaurantData[currentRestaurantIndex].products.forEach(x => {
        productSelect.appendChild(CreateOption(`${x.name}\t(ID: ${x.ID})`, x.ID));
    });
};

cartButton.onclick               = () => sideCartPanel.style.display = sideCartPanel.style.display == 'flex' ? 'none' : 'flex';
checkoutButton.onclick           = () => cart.Checkout();
productManagementOpenBtn.onclick = () => productManagementPanel.style.display = "flex";

productManagementCloseBtn.onclick = () => { 
    form.style.display                   = 'flex';
    errorMessage.innerHTML               = "";
    productManagementPanel.style.display = "none";
};

let priceField = document.querySelector("#price-input input");
priceField.addEventListener('change', () => {
    priceField.value = priceField.value < 0 ? 0 : priceField.value;    
});

productSelect.addEventListener('change', () => FillForm(false));

operationRadios.forEach(radio => radio.addEventListener('change', () => {

    let productDiv      = document.querySelector(".product-management .form-container #product-input");       
    let nameDiv         = document.querySelector(".product-management .form-container #name-input");       
    let descriptionDiv  = document.querySelector(".product-management .form-container #description-input");       
    let priceDiv        = document.querySelector(".product-management .form-container #price-input");       
    
    switch (radio.value)
    {
        case "add":
            productDiv.style.display           = "none";
            nameDiv.style.display              = "flex";
            descriptionDiv.style.display       = "flex";
            priceDiv.style.display             = "flex";
            productDeleteButton.style.display  = "none";
            productSubmitButton.style.display  = "inline-block";
            isUpdate = false;
        break;

        case "update":
            productDiv.style.display           = "flex";
            nameDiv.style.display              = "flex";
            descriptionDiv.style.display       = "flex";
            priceDiv.style.display             = "flex";
            productDeleteButton.style.display  = "none";
            productSubmitButton.style.display  = "inline-block";
            isUpdate = true;
            break;

        case "delete":
            productDiv.style.display           = "flex";
            nameDiv.style.display              = "none";
            descriptionDiv.style.display       = "none";
            priceDiv.style.display             = "none";
            productSubmitButton.style.display  = "none";
            productDeleteButton.style.display  = "inline-block";
            isUpdate = false;
            break;
    }

    FillForm(!isUpdate);

    errorMessage.innerHTML = "";
}));

productSubmitButton.onclick = () =>
{
    const data  = Object.fromEntries(new FormData(form));
    data.id     = parseInt(data.id);
    data.price  = parseFloat(data.price);
    

    if (data.id && isUpdate === true) restaurantData[currentRestaurantIndex].UpdateProduct(data);
    else restaurantData[currentRestaurantIndex].AddProduct(data);

    UpdateDropDownMenu();
};

productDeleteButton.onclick = () => restaurantData[currentRestaurantIndex].RemoveProduct(Object.fromEntries(new FormData(form)).id, UpdateDropDownMenu);
