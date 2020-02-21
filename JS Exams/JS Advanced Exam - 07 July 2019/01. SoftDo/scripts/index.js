// NOTE: The comment sections inside the index.html file is an example of how suppose to be structured the current elements.
//       - You can use them as an example when you create those elements, to check how they will be displayed, just uncomment them.
//       - Also keep in mind that, the actual skeleton in judge does not have this comment sections. So do not be dependent on them!
//       - Тhey are present in the skeleton just to help you!


// This function will be invoked when the html is loaded. Check the console in the browser or index.html file.
function mySolution() {
    let textInput = document.getElementsByTagName('textarea')[0]
    let usernameInput = document.getElementsByTagName('input')[0]
    let btnSend = document.getElementsByTagName('button')[0]
    let pendingQ = document.getElementById('pendingQuestions')
    let openQ = document.getElementById('openQuestions')

    btnSend.addEventListener("click", (e) => {
        if (textInput.value !== '') {
            let div = document.createElement('div')
            div.classList.add('pendingQuestion')

            div.innerHTML =
                `<img src="./images/user.png" width="32" height="32" />` +
                `<span>${usernameInput.value===''?'Anonymous':usernameInput.value}</span>` +
                `<p>${textInput.value}` +
                `</p>` +
                `<div class="actions">` +
                `<button class="archive">Archive</button>` +
                `<button class="open">Open</button>` +
                `</div>`

            pendingQ.appendChild(div)

            textInput.value = ''
            usernameInput.value = ''

        }
    })

    pendingQ.addEventListener("click", (e) => {
        if (e.target.innerHTML === 'Archive') {
            e.target.parentNode.parentNode.remove()
        }
        if (e.target.innerHTML === 'Open') {
            let div = document.createElement('div')
            div.classList.add('openQuestion')

            div.innerHTML =
                `<img src="./images/user.png" width="32" height="32" />` +
                `<span>${e.target.parentNode.parentNode.children[1].innerHTML}</span>` +
                `<p>${e.target.parentNode.parentNode.children[2].innerHTML}` +
                `</p>` +
                `<div class="actions">` +
                `<button class="reply">Reply</button>` +
                `</div>` +
                `<div class="replySection" style="display: none;">` +
                `<input class="replyInput" type="text" placeholder="Reply to this question here...">` +
                `<button class="replyButton">Send</button>` +
                `<ol class="reply" type="1">` +
                `</ol>` +
                `</div>`

            openQ.appendChild(div)
            e.target.parentNode.parentNode.remove()
        }
    })

    openQ.addEventListener("click", (e) => {
        if (e.target.innerHTML === 'Reply') {
            (e.target.parentNode.parentNode.children[4]).style.display = 'block'
            e.target.innerHTML = 'Back'

            let inputTextReply = e.target.parentNode.parentNode.children[4].children[0]
            let listReply = e.target.parentNode.parentNode.children[4].children[2]

            e.target.parentNode.parentNode.children[4].children[1].addEventListener("click", (e) => {
                if(inputTextReply.value!==''){
                    let li = document.createElement('li')
                li.innerHTML = inputTextReply.value

                listReply.appendChild(li)
                inputTextReply.value = ''
                }                
            })

        } else if (e.target.innerHTML === 'Back') {
            (e.target.parentNode.parentNode.children[4]).style.display = 'none'
            e.target.innerHTML = 'Reply'
        }
    })

}





// To check out your solution, just submit mySolution() function in judge system.