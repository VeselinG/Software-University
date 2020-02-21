function solve() {
    const bookName = document.querySelector('form').children[1]
    const yearValue = document.querySelector('form').children[3]
    const priceValue = document.querySelector('form').children[5]
    const btnAddNew = document.querySelector('form').children[6]
    const totalProfit = document.querySelector('body').children[2]
    const bookShelfNew = document.querySelector("body>div>section:nth-child(2)>div")
    const bookShelfOld = document.querySelector("body>div>section:nth-child(1)>div")

    let total = 0;
    btnAddNew.addEventListener("click", function (e) {
        if (bookName.value && Number(yearValue.value) > 0 && Number(priceValue.value) > 0) {
            e.preventDefault()

            const div = document.createElement('div')
            div.classList.add('book')

            const p = document.createElement('p')
            p.innerHTML = `${bookName.value} [${Number(yearValue.value)}]`
            div.appendChild(p)

            const buttonBuy = document.createElement('button')
            const price = Number(priceValue.value).toFixed(2);
            buttonBuy.innerHTML = `Buy it only for ${Number(yearValue.value) >= 2000?price:(price*0.85).toFixed(2)} BGN`
            div.appendChild(buttonBuy)

            if (Number(yearValue.value) >= 2000) {
                const buttonMove = document.createElement('button')
                buttonMove.innerHTML = `Move to old section`
                div.appendChild(buttonMove)

                bookShelfNew.appendChild(div)

                buttonMove.addEventListener("click", function(e){
                   const currentDiv = e.target.parentNode
                   const currentPrice = Number(currentDiv.children[1].innerHTML.split(' ')[4])
                   currentDiv.children[1].innerHTML = `Buy it only for ${(currentPrice*0.85).toFixed(2)} BGN`
                   currentDiv.children[2].remove()
                   
                   bookShelfOld.appendChild(currentDiv)
                   
                })
            } else {
                bookShelfOld.appendChild(div)
            }  
            
            buttonBuy.addEventListener("click",function(e){
                const currentPrice = Number(e.target.innerHTML.split(' ')[4])
                total += currentPrice                
                totalProfit.innerHTML =`Total Store Profit: ${total.toFixed(2)} BGN` 

                e.target.parentNode.remove()               
            })

            bookName.value = ''
            yearValue.value = ''
            priceValue.value = ''
        }
    })
}