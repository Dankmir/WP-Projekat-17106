import { Product    } from "./product.js";

export class Restaurant
{
    constructor(data)
    {
        this.ID           = data.id;
        this.ime          = data.name;
        this.address      = data.address;
        this.phoneNumber  = data.phoneNumber;
        this.products     = [];
          
        this.cart         = document.cart;
        this.host         = document.querySelector(".product-list");
        this.errorMessage = document.querySelector(".product-management .error-message");
    }

    
    Add(data)
    {
        this.products.push(new Product(data, this.cart));
    }

    AddProduct(data)
    {
        if (data.name == "" || data.description == "" || !data.price)
        {
            this.errorMessage.innerHTML = "All fields must be filled!";
            return;
        }

        let obj = { 
            name :          data.name,
            description :   data.description,
            price :         data.price ? parseFloat(data.price) : 0
        };
    
        fetch(`https://localhost:5001/Restaurant/AddProduct/${this.ID}`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(obj)
            }).then(res => {
                if (res.ok)
                {
                    res.json().then(id => { 
                        obj.id = id; 
                        this.Add(obj);

                        let option = document.createElement("option");
                        option.value = obj.id;
                        option.innerHTML = `${obj.name}\t(ID: ${obj.id})`;
                        document.querySelector(".product-select").appendChild(option);
                        this.errorMessage.innerHTML = "";

                        this.Draw();
                    })
                }
                else if (res.status == 400)
                {
                    res.json().then(p => this.errorMessage.innerHTML = p.message);
                }
                    
            }
        ).catch(x => console.log(x));
    }

    RemoveProduct(ID, Callback)
    {
        fetch(`https://localhost:5001/Restaurant/DeleteProduct/${ID}`, {
            method: "DELETE",
        }).then(res => {
            if (res.ok)
            {
                this.products = this.products.filter(x => x.ID != ID);
                this.Draw(this.host);
                Callback();
                this.cart.RemoveLocal(ID);
            }               
        }).catch(x => console.log(x));
    }

    UpdateProduct(data)
    {
        if (data.name == "" || data.description == "" || !data.price)
        {
            this.errorMessage.innerHTML = "All fields must be filled!";
            return;
        }

        fetch("https://localhost:5001/Restaurant/UpdateProduct", {
            method: "PUT",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(data)
            }).then(res => {
                if (res.ok)
                {
                    let product = this.products.find(x => x.ID == data.id);
                    
                    if (product)
                    {
                        product.Update(data);
                    
                        this.Draw(this.host);
                        
                        this.errorMessage.innerHTML = "";
                    }
                    else console.log("Product does not exist.");
                }
                else if (res.status == 400)
                {
                    res.json().then(p => this.errorMessage.innerHTML = p.message);
                }
            }
        ).catch(x => console.log(x));
    }

    Draw()
    {
        if (!this.host) throw Error("Host is undefined.");

        this.host.innerHTML = "";
        this.products.forEach(x => x.Draw(this.host));
    }
}