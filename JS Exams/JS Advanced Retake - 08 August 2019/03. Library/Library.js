class Library {
    constructor(libraryName) {
        this.libraryName = libraryName;
        this.subscribers = [];
        this.subscriptionTypes = {
            normal: libraryName.length,
            special: libraryName.length * 2,
            vip: Number.MAX_SAFE_INTEGER
        }
    }

    subscribe(name, type) {

        if (!Object.keys(this.subscriptionTypes).includes(type)) {
            throw new Error(`The type ${type} is invalid`)
        }

        let subscriberName = this.subscribers.find(obj => obj.name === name)

        if (subscriberName) {
            subscriberName.type = type;
        } else {
            subscriberName = {
                name,
                type,
                books: []
            }
            this.subscribers.push(subscriberName)
        }
        return subscriberName
    }

    unsubscribe(name) {
        let person = this.subscribers.find(obj => obj.name === name)

        if (person) {
            let index = this.subscribers.indexOf(person)
            this.subscribers.splice(index, 1)
        } else {
            throw new Error(`There is no such subscriber as ${name}`)
        }

        return this.subscribers
    }

    receiveBook(subscriberName, bookTitle, bookAuthor) {
        let person = this.subscribers.find(obj => obj.name === subscriberName)
        if (!person) {
            throw new Error(`There is no such subscriber as ${name}`)
        } else {
            let subType = person.type
            let booksCount = person.books.length

            if (booksCount < subType) {
                let obj = {
                    bookTitle,
                    bookAuthor
                }
                person.books.push(obj)
            } else {
                throw new Error (`You have reached your subscription limit ${subType}!`)
            }
        }
        return person
    }
}