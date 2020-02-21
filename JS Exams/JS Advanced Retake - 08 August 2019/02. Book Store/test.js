let BookStore = require('./02. Book Store_Ресурси');
const assert = require('chai').assert;
const beforeEach = require('mocha').beforeEach;

describe('BookStore Test', function () {
    let obj;
    beforeEach(function () {
        obj = new BookStore('Terminator');
    })

    describe("Test constructor", function () {
        it("name => name", function () {
            assert.equal(obj.name, 'Terminator');
        });
        it("books => []", function () {
            assert.deepEqual(obj.books, []);
        });
        it("_workers => []", function () {
            assert.deepEqual(obj._workers, []);
        });
    });

    describe("Test get workers()", function () {
        it("Test if workers are empty", function () {
            assert.deepEqual(obj.workers, []);
        });
    });

    describe("Test stockBooks()", function () {
        it("Test books length", function () {
            obj.stockBooks(['Inferno-Dan Braun', 'Harry Potter-J.Rowling', 'Uncle Toms Cabin-Hariet Stow', 'The Jungle-Upton Sinclear'])
            assert.equal(4, obj.books.length);
        });
        it("Test books [0]", function () {
            obj.stockBooks(['Inferno-Dan Braun', 'Harry Potter-J.Rowling', 'Uncle Toms Cabin-Hariet Stow', 'The Jungle-Upton Sinclear'])
            assert.deepEqual({
                author: "Dan Braun",
                title: "Inferno"
            }, obj.books[0]);
        });
        it("Test books return", function () {
            assert.deepEqual(obj.stockBooks(['Inferno-Dan Braun', 'Harry Potter-J.Rowling', 'Uncle Toms Cabin-Hariet Stow', 'The Jungle-Upton Sinclear']), obj.books);
        });
    });

    describe("Test hire()", function () {
        it("Test if worker is new", function () {
            obj.hire('Vesko', 'Manager')
            assert.equal(obj.workers.length, 1);
            assert.deepEqual(obj.workers[0], {
                name: 'Vesko',
                position: 'Manager',
                booksSold: 0
            });
        });
        it("Test return message if worker is new", function () {
            assert.equal(obj.hire('Vesko', 'Manager'), `Vesko started work at Terminator as Manager`)
        });
        it("Should throw Error if worker is hire", function () {
            obj.hire('Vesko', 'Manager')
            assert.throws(() => obj.hire('Vesko', 'Manager'), (Error, 'This person is our employee'))
        });
    });

    describe("Test fire()", function () {
        it("Should throw Error if worker doesn't work here", function () {
            obj.hire('Vesko', 'Manager')
            assert.throws(()=>obj.fire('Toshko'), (Error,"Toshko doesn't work here"));          
        });
        it("Should fire worker correctly", function () {
            obj.hire('Vesko', 'Manager')
            obj.hire('Toshko', 'Saler')
            obj.fire('Vesko')
            assert.equal(obj.workers.length,1);          
        });
        it("Should return fire message", function () {
            obj.hire('Vesko', 'Manager')
            obj.hire('Toshko', 'Saler')            
            assert.equal(obj.fire('Vesko'),'Vesko is fired');          
        });
    });

    describe("Test sellBook()", function () {
        it("Should throw Error if there is no such a book", function () {
            obj.stockBooks(['Inferno-Dan Braun', 'Harry Potter-J.Rowling', 'Uncle Toms Cabin-Hariet Stow', 'The Jungle-Upton Sinclear'])
            assert.throws(()=>obj.sellBook('Rambo','Vesko'), (Error,"This book is out of stock"));          
        });
        it("Should throw Error if there is no such a worker", function () {
            obj.stockBooks(['Inferno-Dan Braun', 'Harry Potter-J.Rowling', 'Uncle Toms Cabin-Hariet Stow', 'The Jungle-Upton Sinclear'])
            obj.hire('Toshko','Saler')
            assert.throws(()=>obj.sellBook('Inferno','Vesko'), (Error,"Vesko is not working here"));          
        });
        it("Test sell Book correctly", function () {
            obj.stockBooks(['Inferno-Dan Braun', 'Harry Potter-J.Rowling', 'Uncle Toms Cabin-Hariet Stow', 'The Jungle-Upton Sinclear'])
            obj.hire('Toshko','Saler')
            obj.sellBook('Inferno','Toshko')
            assert.equal(obj.books.length, 3);          
            assert.equal(obj.workers[0].booksSold, 1);          
        });
    });

    describe("Test printWorkers()", function () {       
        it("Test printWorkers() return message", function () {
            obj.stockBooks(['Inferno-Dan Braun', 'Harry Potter-J.Rowling', 'Uncle Toms Cabin-Hariet Stow', 'The Jungle-Upton Sinclear'])
            obj.hire('Toshko','Saler')
            obj.hire('Vesko','Manager')
            obj.sellBook('Inferno','Toshko')
            assert.equal(obj.printWorkers(), "Name:Toshko Position:Saler BooksSold:1\nName:Vesko Position:Manager BooksSold:0");          
                      
        });

        it('test witout workers', () => {
            assert.equal(obj.printWorkers(), "");
        })
    });

});