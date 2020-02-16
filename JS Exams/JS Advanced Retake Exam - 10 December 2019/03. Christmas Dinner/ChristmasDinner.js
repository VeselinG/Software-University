class ChristmasDinner {

    constructor(budget) {
        if (budget < 0) {
            throw new Error("The budget cannot be a negative number");
        }
        this.budget = budget;
        this.dishes = [];
        this.products = [];
        this.guests = {};
    }

    shopping(products) {
        let type = products[0];
        let price = +products[1];

        if (this.budget < price) {
            throw new Error("Not enough money to buy this product")
        } else {
            this.products.push(type);
            this.budget -= price;
            return `You have successfully bought ${type}!`
        }
    }

    recipes(recipe) {
        let recipeName = recipe.recipeName;
        let productsList = recipe.productsList;

        let checker = productsList.every(product => this.products.includes(product))
        if (checker) {
            this.dishes.push({
                recipeName,
                productsList
            });
            return `${recipeName} has been successfully cooked!`;

        } else {
            throw new Error("We do not have this product");
        }
    }

    inviteGuests(name, dish) {
        let checkDish = this.dishes.find(obj => obj.recipeName === dish)
        let checkGuestName = this.guests.hasOwnProperty(name)

        if (!checkDish) {
            throw new Error("We do not have this dish");
        } else if (checkGuestName) {
            throw new Error("This guest has already been invited");
        } else {
            this.guests[`${name}`] = dish;
            return `You have successfully invited ${name}!`
        }
    }

    showAttendance() {
        const names = Object.keys(this.guests);
        let result = '';

        for (let guestName of names) {
            const questDish = this.guests[guestName];
            const questProducts = this.dishes.find(obj => obj.recipeName === questDish);

            result += `${guestName} will eat ${questDish}, which consists of ${questProducts.productsList.join(', ')}\n`
        }

        return result.substring(0,result.length-1);
    }
}

let dinner = new ChristmasDinner(300);

dinner.shopping(['Salt', 1]);
dinner.shopping(['Beans', 3]);
dinner.shopping(['Cabbage', 4]);
dinner.shopping(['Rice', 2]);
dinner.shopping(['Savory', 1]);
dinner.shopping(['Peppers', 1]);
dinner.shopping(['Fruits', 40]);
dinner.shopping(['Honey', 10]);

dinner.recipes({
    recipeName: 'Oshav',
    productsList: ['Fruits', 'Honey']
});
dinner.recipes({
    recipeName: 'Folded cabbage leaves filled with rice',
    productsList: ['Cabbage', 'Rice', 'Salt', 'Savory']
});
dinner.recipes({
    recipeName: 'Peppers filled with beans',
    productsList: ['Beans', 'Peppers', 'Salt']
});

dinner.inviteGuests('Ivan', 'Oshav');
dinner.inviteGuests('Petar', 'Folded cabbage leaves filled with rice');
dinner.inviteGuests('Georgi', 'Peppers filled with beans');

console.log(dinner.showAttendance());