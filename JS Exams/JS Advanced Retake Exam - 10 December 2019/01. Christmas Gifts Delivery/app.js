function solution() {

    const addInput = document.querySelector("body > div > section:nth-child(1) > div > input[type=text]");
    const addGiftButton = document.querySelector("body > div > section:nth-child(1) > div > button");
    const listOfGifts = document.querySelector("body > div > section:nth-child(2) > ul");
    const listOfSendGifts = document.querySelector("body > div > section:nth-child(3) > ul");
    const listOfDiscardGifts = document.querySelector("body > div > section:nth-child(4) > ul");

    addGiftButton.addEventListener("click", addGift)

    function addGift() {
        //Shold not work with empty input
        if(addInput.value===''){
            return
        }
        //Create new Elements for 'li' and buttons forEach li
        const giftLi = createNewElement('li', addInput.value);
        const sendButton = createNewElement('button', 'Send', 'sendButton');
        const discardButton = createNewElement('button', 'Discard', 'discardButton');

        //Create new gift 
        giftLi.classList.add('gift');
        giftLi.appendChild(sendButton);
        giftLi.appendChild(discardButton);

        //Append gift to List of Gifts
        listOfGifts.appendChild(giftLi);

        //Sort List of Gifts. 1) Delete current listOfGifts 2)appendChild on sorted listOfGifts (sortedList)
        const sortedList = Array.from(listOfGifts.children).sort((a, b) => a.innerHTML.localeCompare(b.innerHTML));
        listOfGifts.innerHTML = ''
        sortedList.forEach(el => listOfGifts.appendChild(el));

        //clear the input field
        addInput.value = ''

        //When send button is clicked. Remove from List of Gifts and go to Send List
        sendButton.addEventListener("click",(el)=>{
            const parent = el.target.parentNode;
            const text = parent.childNodes[0].textContent;
            const sendGiftLi = createNewElement('li',text);
            listOfSendGifts.appendChild(sendGiftLi);
            listOfGifts.removeChild(parent);                                        
        })

        //When discard button is clicked. Remove from List of Gifts and go to Discard List
        discardButton.addEventListener("click",(el)=>{
            const parent = el.target.parentNode;
            const text = parent.childNodes[0].textContent;
            const sendGiftLi = createNewElement('li',text);            
            listOfDiscardGifts.appendChild(sendGiftLi)
            listOfGifts.removeChild(parent)                                        
        })
    }

    function createNewElement(el, text, id) {
        const element = document.createElement(el);
        if (text) {
            element.innerHTML = text;
        }
        if (id) {
            element.id = id;
        }

        return element
    }

}