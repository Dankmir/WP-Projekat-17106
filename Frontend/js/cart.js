export class Cart
{
    constructor()
    {
        document.cart       = this;
        this.cartContent    = [];
        this.cartTotal      = 0;
        this.host           = document.querySelector(".cart-content");
        this.cartTotalText  = document.querySelector(".cart-total h3");
    }

    AddLocal(product)
    {
        this.cartContent.push(product);
        this.UpdateCartTotal();
    }

    Add(product)
    {
        fetch(`https://localhost:5001/Restaurant/AddCartItem/${product.ID}`, {
            method: 'POST'
            }).then(res => {
                if (res.ok)
                {
                    let item = this.cartContent.find(x => x.ID == product.ID);

                    if (!item)
                        this.cartContent.push(item = product);
                    
                    res.json().then(quantity => {
                        item.quantity = quantity;
                        this.UpdateCartTotal();
                    });
                }   
            }
        );
    }

    RemoveLocal(ID)
    {
        this.cartContent = this.cartContent.filter(x => ID != x.ID);
        this.Draw();
    }

    Remove(ID)
    {
        return fetch(`https://localhost:5001/Restaurant/DeleteCartItem/${ID}`, {
            method: 'DELETE'
            }).then(res => {
                if (res.ok)
                {
                    return res.ok;
                }
                else if (res.status == 400)
                {
                    res.json().then(p => console.log(p.message));
                }
            }).catch(() => false);
    }

    UpdateCartTotal()
    {
        this.cartTotal = this.cartContent.reduce((acc, el) => {
            return acc + el.GetTotalPrice();
        }, 0);

        this.cartTotalText.innerHTML = `Total: ${this.cartTotal.toFixed(2)} rsd`;

        this.Draw();
    }

    Checkout()
    {
        if (this.cartContent.length > 0)
        {
            let closeCheckoutDialog = document.querySelector(".close-dialog button");
            let fullscreenPanel     = document.querySelector(".fullscreen-panel");

            fullscreenPanel.style.display = fullscreenPanel.style.display = 'flex';
            document.body.style.overflowY = document.body.style.overflowY ='hidden';

            document.querySelector('#total-price').innerHTML = this.cartTotal.toFixed(2);
            
            let time = Math.floor(Math.random() * 100);
            if (time < 30) time *= 2;
            document.querySelector('#arrival-time').innerHTML = time;  

            closeCheckoutDialog.onclick = () => {
                fullscreenPanel.style.display = fullscreenPanel.style.display = 'none';
                document.body.style.overflowY = document.body.style.overflowY = 'scroll';
                this.Clear();
            }
        }
    }

    Clear()
    {
        return fetch(`https://localhost:5001/Restaurant/DeleteAllCartItem`, {
            method: 'DELETE'
            }).then(res => {
                if (res.ok)
                {
                    this.cartContent.forEach(x => this.RemoveLocal(x.ID));
                }
            }).catch(() => false);
    }

    Draw()
    {
        if (!this.host) throw Error("Host is undefined.");

        this.host.innerHTML = "";
        this.cartContent.forEach(x => x.Draw(this.host));

        let cartItems = this.host.childNodes;
        let counter = 1;
        cartItems.forEach(x => {
            if (counter++ % 2 == 0) 
                x.classList.add("cart-item-back");            
        });
    }    
}