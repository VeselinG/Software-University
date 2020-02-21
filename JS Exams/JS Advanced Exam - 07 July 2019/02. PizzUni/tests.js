const PizzUni = require('./02. PizzUni_Ресурси');
const assert = require('chai').assert;
const beforeEach = require('mocha').beforeEach;

describe("Test PizzUni", function () {
    let obj;
    beforeEach(function () {
        obj = new PizzUni;
    })

    describe("Test constructor", function () {
        it("registeredUsers=>[]", function () {
            assert.deepEqual(obj.registeredUsers, []);
        });
        it("availableProducts => {}", function () {
            assert.deepEqual(obj.availableProducts, {
                pizzas: ['Italian Style', 'Barbeque Classic', 'Classic Margherita'],
                drinks: ['Coca-Cola', 'Fanta', 'Water']
            });
        });
        it("orders => []", function () {
            assert.deepEqual(obj.orders, []);
        });
    });

    describe("Test registerUser", function () {
        it("Should throw Error if email is in collection", function () {
            obj.registerUser("vg2@gmail.com");
            assert.throws(() => obj.registerUser("vg2@gmail.com"), (Error, 'This email address (vg2@gmail.com) is already being used!'));
        });
        it("Should add email in collection", function () {
            obj.registerUser("vg2@gmail.com")
            assert.equal(1, obj.registeredUsers.length);
            assert.equal("vg2@gmail.com", obj.registeredUsers[0].email);
            assert.deepEqual([], obj.registeredUsers[0].orderHistory);
            assert.equal(2, Object.keys(obj.registeredUsers[0]).length);
        });
        it("Should create obj correctly", function () {
            obj.registerUser("vg2@gmail.com")
            assert.equal("email", Object.keys(obj.registeredUsers[0])[0]);
            assert.equal("orderHistory", Object.keys(obj.registeredUsers[0])[1]);
            assert.equal("vg2@gmail.com", Object.values(obj.registeredUsers[0])[0]);
            assert.deepEqual([], Object.values(obj.registeredUsers[0])[1]);
        });
    });

    describe("Test makeAnOrder", function () {
        it("Should throw Error if email is NOT in collection", function () {
            obj.registerUser("vg2@gmail.com");
            assert.throws(() => obj.makeAnOrder("vg2@abv.com", "Italian Style", "Coca-Cola"),
                (Error, 'You must be registered to make orders!'));
        });
        it("Should throw Error if pizza is NOT in collection", function () {
            obj.registerUser("vg2@gmail.com");
            assert.throws(() => obj.makeAnOrder("vg2@gmail.com", "Bulgarian Style", "Coca-Cola"),
                (Error, 'You must order at least 1 Pizza to finish the order.'));
        });
        it("If drink is NOT in collection", function () {
            obj.registerUser("vg2@gmail.com");
            obj.makeAnOrder("vg2@gmail.com", "Italian Style", "Airan")
            assert.deepEqual(Object.values(obj.registeredUsers[0])[1], [{
                orderedPizza: 'Italian Style'
            }]);
        });
        it("If drink is in collection", function () {
            obj.registerUser("vg2@gmail.com");
            obj.makeAnOrder("vg2@gmail.com", "Italian Style", "Coca-Cola")
            assert.deepEqual(Object.values(obj.registeredUsers[0])[1],
                [{
                    orderedPizza: 'Italian Style',
                    orderedDrink: 'Coca-Cola'
                }]);
        });
        it("If drink is NOT in collection ORDERS", function () {
            obj.registerUser("vg2@gmail.com");
            obj.makeAnOrder("vg2@gmail.com", "Italian Style", "Airan")
            assert.deepEqual(Object.values(obj.registeredUsers[0])[1], [{
                orderedPizza: 'Italian Style'
            }]);
            assert.equal(obj.orders.length, 1)
            assert.deepEqual(obj.orders[0], {
                orderedPizza: 'Italian Style',
                email: "vg2@gmail.com",
                status: 'pending'
            })
        });
        it("If drink is in collection ORDERS", function () {
            obj.registerUser("vg2@gmail.com");
            obj.makeAnOrder("vg2@gmail.com", "Italian Style", "Coca-Cola")
            assert.deepEqual(Object.values(obj.registeredUsers[0])[1],
                [{
                    orderedPizza: 'Italian Style',
                    orderedDrink: 'Coca-Cola'
                }]);

            assert.equal(obj.orders.length, 1)
            assert.deepEqual(obj.orders[0], {
                orderedPizza: 'Italian Style',
                orderedDrink: 'Coca-Cola',
                email: "vg2@gmail.com",
                status: 'pending'
            })
        });
    });

    describe("Test detailsAboutMyOrder", function () {
        it("If orderID is in collection ORDERS", function () {
            obj.registerUser("vg2@gmail.com");
            obj.makeAnOrder("vg2@gmail.com", "Italian Style", "Coca-Cola")
            assert.equal(obj.detailsAboutMyOrder(0), "Status of your order: pending")
        });
        it("If orderID is NOT in collection ORDERS", function () {
            obj.registerUser("vg2@gmail.com");
            obj.makeAnOrder("vg2@gmail.com", "Italian Style", "Coca-Cola")
            assert.equal(obj.detailsAboutMyOrder(1), undefined)
        });
    });

    describe("Test completeOrder", function () {
        it("If orders>0", function () {
            obj.registerUser("vg2@gmail.com");
            obj.makeAnOrder("vg2@gmail.com", "Italian Style", "Coca-Cola")
            assert.deepEqual(obj.completeOrder(), {
                orderedPizza: 'Italian Style',
                orderedDrink: 'Coca-Cola',
                email: "vg2@gmail.com",
                status: 'completed'
            })
        });
        it("If orders<=0", function () {
            obj.registerUser("vg2@gmail.com");
            //obj.makeAnOrder("vg2@gmail.com", "Italian Style", "Coca-Cola")
            assert.deepEqual(obj.completeOrder(), undefined)
        });
    });

})