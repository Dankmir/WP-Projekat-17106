import { CartItem } from "./cartItem.js";

export class Product
{
    constructor({id, name, description, price})
    {
        this.ID          = id;
        this.name        = name;
        this.description = description;
        this.price       = price;
        this.content     = null;
        this.cart        = document.cart; 
    }

    Update(data)
    {
        this.ID          = data.id;
        this.name        = data.name;
        this.description = data.description;
        this.price       = data.price;
    }

    Draw(host)
    {
        if (!host) throw Error("Host is undefined.");

        this.content = host;
        
        this.imagePath  = "./img/" + this.name + ".png";

        /* --- MAIN CONTAINER --- */
        
        let productDiv = document.createElement("div");
        productDiv.classList.add("product");
        host.appendChild(productDiv);

        /* ------------------------------------------------- */
        /* --- IMAGE --- */ 

        let imageDiv = document.createElement("div");
        imageDiv.classList.add("product-image");
        productDiv.appendChild(imageDiv);

        let productImage = document.createElement("img");
        productImage.src = this.imagePath;
        imageDiv.appendChild(productImage);

        /* ------------------------------------------------- */
        /* --- INFO --- */

        let infoDiv = document.createElement("div");
        infoDiv.classList.add("product-info");
        productDiv.appendChild(infoDiv);

        // Product name
        let nameDiv = document.createElement("div");
        nameDiv.classList.add("product-name");
        infoDiv.appendChild(nameDiv);

        let nameText = document.createElement("h1");
        nameText.innerHTML = this.name;
        nameDiv.appendChild(nameText);
    
        infoDiv.appendChild(document.createElement("hr"));

        // Description
        let descriptionDiv = document.createElement("div");
        descriptionDiv.classList.add("product-description");
        infoDiv.appendChild(descriptionDiv);
        
        let descriptionText = document.createElement("p");
        descriptionText.innerHTML = this.description;
        descriptionDiv.appendChild(descriptionText);

        /* ------------------------------------------------- */
        /* --- ADD TO CART --- */
        let addToCartDiv = document.createElement("div");
        addToCartDiv.classList.add("product-cart");
        productDiv.appendChild(addToCartDiv);

        let btn = document.createElement("button");
        let cartImage = document.createElement("img");
        cartImage.src = "./img/shopping-cart.png";
        btn.appendChild(cartImage);
        
        btn.onclick = () => this.cart.Add(new CartItem(this));
        
        addToCartDiv.appendChild(btn);

        let priceText = document.createElement("h3");
        priceText.innerHTML = `${this.price.toFixed(2)} rsd`;
        addToCartDiv.appendChild(priceText);

        /* ------------------------------------------------- */
    }
    
}

/*
<div class ="product">
    <div class = "product-image">
        <img src="./img/burger.jpg" alt="">
    </div>
    <div class = "product-info">
        <div class = "product-name">
            <h1>Smashburger</h1>
            <h3>$0.99</h3>
        </div>
        <hr>
        <div class = "product-description">
            <p>Zemička, zelena salata, 180 grama junećeg mesa, pančeta, čedar sir, karamelizovani luk, krastavčići, ned-senf, pomfrit</p>
        </div>
    </div>
    <div class = "product-cart">
        <button>
            <img src="./img/shopping-cart.png" alt="">
        </button>
    </div>
</div>
*/