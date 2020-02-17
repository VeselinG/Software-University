function solve() {
    const addForm = document.getElementById('add-new');
    const availableProducts = document.querySelector("#products > ul")
    const myProducts = document.querySelector("#myProducts > ul")
    const totalPrice = document.getElementsByTagName('h1')[1]
    const buyButton = document.getElementsByTagName('button')[2]
    const filterButton = document.getElementsByTagName('button')[0]

    let total = 0;

    addForm.children[4].addEventListener('click', function (e) {
        e.preventDefault()

        const name = addForm.children[1];
        const quantity = addForm.children[2];
        const price = addForm.children[3];

        if (name.value !== '' && Number(quantity.value) > 0 && Number(price.value) > 0) {

            let li = createNewElement('li')
            let span = createNewElement('span', name.value)
            let strong = createNewElement('strong', `Available: ${Number(quantity.value)}`)
            let div = createNewElement('div')
            let strongDiv = createNewElement('strong', Number(price.value).toFixed(2))
            let button = createNewElement('button', "Add to Client's List")

            li.appendChild(span)
            li.appendChild(strong)
            li.appendChild(div)
            div.appendChild(strongDiv)
            div.appendChild(button)
            availableProducts.appendChild(li);

            filterButton.addEventListener("click", filter)
            button.addEventListener("click", addProduct)
            buyButton.addEventListener("click", clearAll)
        }
        name.value = ''
        quantity.value = ''
        price.value = ''
    })

    function filter() {
        const filterValue = document.getElementsByClassName('filter')[0].children[1]
        Array.from(availableProducts.children).map(li => {
            if (li.children[0].innerHTML.includes(filterValue.value)) {
                li.style.display = 'block';
            } else {
                li.style.display = 'none';
            }
        })
    }

    function clearAll() {
        totalPrice.innerHTML = `Total Price: 0.00`
        myProducts.innerHTML = ''
        total = 0;
    }

    function addProduct() {
        const productName = this.parentNode.parentNode.children[0].innerHTML
        const productPrice = this.parentNode.children[0].innerHTML
        let productQuantity = Number(this.parentNode.parentNode.children[1].innerHTML.split(': ')[1])

        const li = createNewElement('li', productName)
        const strong = createNewElement('strong', Number(productPrice).toFixed(2))
        li.appendChild(strong)
        myProducts.appendChild(li)

        total += Number(productPrice)
        totalPrice.innerHTML = `Total Price: ${total.toFixed(2)}`
        productQuantity--

        if (productQuantity === 0) {
            this.parentNode.parentNode.remove()
        } else {
            this.parentNode.parentNode.children[1].innerHTML = `Available: ${Number(productQuantity)}`
        }
    }

    function createNewElement(el, text, value) {
        const element = document.createElement(el);
        if (text) {
            element.innerHTML = text;
        }
        if (value) {
            element.value = value;
        }
        return element
    }
}