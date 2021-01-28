import { Clamp } from "./helpers.js";

export class CartItem
{
    constructor({ID, name, price}, quantity)
    {
        this.ID             = ID;
        this.name           = name;
        this.basePrice      = price;
        this.quantity       = quantity;
        
        this.cart           = document.cart;
        this.content        = null;
        this.quantityText   = null;   
        this.count          = 0; 
    }

    GetTotalPrice()
    {
        return this.basePrice * this.quantity;
    }

    Increase()
    {
        this.quantityText.innerHTML = 'x' + this.quantity;
        this.cart.UpdateCartTotal();
    }
    
    Decrease()
    {
        this.quantityText.innerHTML = 'x' + --this.quantity;
        
        if (this.quantity == 0)
        {
            this.cart.cartContent = this.cart.cartContent.filter(x => x != this);
        }
        
        this.cart.UpdateCartTotal();
    }

    Draw(host)
    {
        if (!host) throw Error("Host is undefined.");

        this.content = host;

        let cartItemDiv = document.createElement("div");
        cartItemDiv.classList.add("cart-item");
        host.appendChild(cartItemDiv);

        /* --- NAME --- */
        let nameDiv = document.createElement("div");
        nameDiv.classList.add("cart-item-name");
        cartItemDiv.appendChild(nameDiv);

        let nameText = document.createElement("h3");
        nameText.innerHTML = this.name;
        nameDiv.appendChild(nameText);

        /* ------------------------------------------------- */
        /* --- QUANTITY --- */
        let buttonsDiv = document.createElement("div");
        buttonsDiv.classList.add("cart-item-buttons");
        cartItemDiv.appendChild(buttonsDiv);

        let btn = document.createElement("button");
        btn.classList.add("cart-item-button-decrease");
        btn.innerHTML = "-";

        btn.onclick = async () => { 
            if (await document.cart.Remove(this.ID))
                this.Decrease();
        };

        buttonsDiv.appendChild(btn);

        let quantityText = document.createElement("h4");
        quantityText.innerHTML = 'x' + this.quantity;
        this.quantityText = quantityText;
        buttonsDiv.appendChild(quantityText);


        btn = document.createElement("button");
        btn.classList.add("cart-item-button-increase");
        btn.innerHTML = "+";

        btn.onclick = () => document.cart.Add(this);
        
        buttonsDiv.appendChild(btn);
    }
}

/*
<div class = "cart-item">
    <div class = "cart-item-name">
        <h3>Smashburger</h3>
    </div>

    <div class = "cart-item-buttons">
        <button class = "cart-item-button-decrease">-</button>
        <h4>x1</h4>
        <button class = "cart-item-button-increase">+</button>
    </div>
</div>
*/