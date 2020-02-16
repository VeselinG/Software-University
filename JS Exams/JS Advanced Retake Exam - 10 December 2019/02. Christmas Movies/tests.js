const ChristmasMovies = require('./02. Christmas Movies_Resources');
const assert = require('chai').assert;
const beforeEach = require('mocha').beforeEach;

describe("Test ChristmasMovies", function () {
    let obj;
    beforeEach(function () {
        obj = new ChristmasMovies;
    })

    describe("Test constructor", function () {
        it("movieCollection => []", function () {
            assert.deepEqual(obj.movieCollection, []);
        });
        it("watched => {}", function () {
            assert.deepEqual(obj.watched, {});
        });
        it("actors => []", function () {
            assert.deepEqual(obj.actors, []);
        });
    });

    describe("Test buyMovie()", function () {
        it("Should add movie in collection if new", function () {
            assert.equal(obj.buyMovie('Rambo', ['Sylvester', 'Stallone']), 'You just got Rambo to your collection in which Sylvester, Stallone are taking part!');
        });
        it("Should throw Error if movie in collection is not new", function () {
            obj.buyMovie('Rambo', ['Sylvester', 'Stallone']);
            assert.throws(() => obj.buyMovie('Rambo', ['Sylvester', 'Stallone']), (Error, 'You already own Rambo in your collection!'));
        });
        it("Add movie name and actors in collection", function () {
            obj.buyMovie('Rambo', ['Sylvester', 'Stallone']);
            assert.equal(obj.movieCollection[0].name, 'Rambo');
            assert.deepEqual(obj.movieCollection[0].actors, ['Sylvester', 'Stallone']);
        });
        it("Add only unique actors in collection", function () {
            obj.buyMovie('Rambo', ['Sylvester', 'Sylvester', 'Stallone']);
            assert.deepEqual(obj.movieCollection[0].actors, ['Sylvester', 'Stallone'])
        });
    });

    describe("Test  discardMovie()", function () {
        it("Should throw Error if movie is not in the collection", function () {
            assert.throws(() => obj.discardMovie('Rambo'), (Error, 'Rambo is not at your collection!'));
        });
        it("Should delete Movie from collection and watched from obj", function () {
            obj.buyMovie('Rambo', ['Sylvester', 'Stallone']);
            obj.watchMovie('Rambo')
            assert.equal(obj.discardMovie('Rambo'), 'You just threw away Rambo!');
        });
        it("Should throw Error if movie is in collection but is not watched", function () {
            obj.buyMovie('Rambo', ['Sylvester', 'Stallone']);
            assert.throws(() => obj.discardMovie('Rambo'), (Error, 'Rambo is not watched!'));
        });
        //Check if this is need it ?
        // it("Check movie collection after discard", function() {            
        //     obj.buyMovie('Rambo',['Sylvester','Stallone']);
        //     obj.watchMovie('Rambo')
        //     obj.discardMovie('Rambo')        
        //     assert.deepEqual(obj.movieCollection,[]);
        // });
        // it("Check whatch collection after discard", function() {            
        //     obj.buyMovie('Rambo',['Sylvester','Stallone']);
        //     obj.watchMovie('Rambo')
        //     obj.discardMovie('Rambo')        
        //     assert.deepEqual(obj.watched,{});
        // });
    });

    describe("Test  watchMovie()", function () {
        it("Should throw Error if movie is not in the collection", function () {
            assert.throws(() => obj.watchMovie('Rambo'), (Error, 'No such movie in your collection!'));
        });
        it("Whatch movie for first time", function () {
            obj.buyMovie('Rambo',['Sylvester','Stallone']);
            obj.watchMovie('Rambo');
            assert.equal(obj.watched['Rambo'],1)
        });
        it("Whatch movie for second time", function () {
            obj.buyMovie('Rambo',['Sylvester','Stallone']);
            obj.watchMovie('Rambo');
            obj.watchMovie('Rambo');
            assert.equal(obj.watched['Rambo'],2)
        });       
    });

    describe("Test  favouriteMovie()", function () {
        it("Should throw Error if whatch collection is empty", function () {
            assert.throws(() => obj.favouriteMovie(), (Error, 'You have not watched a movie yet this year!'));
        });
        it("Should return favorite movie message", function () {
            obj.buyMovie('Rambo',['Sylvester','Stallone']);
            obj.buyMovie('Terminator',['Arnold']);
            obj.watchMovie('Rambo');
            obj.watchMovie('Terminator');
            obj.watchMovie('Terminator');
            assert.equal(obj.favouriteMovie(), 'Your favourite movie is Terminator and you have watched it 2 times!');
        });         
    });

    describe("Test  mostStarredActor()", function () {
        it("Should throw Error if whatch collection is empty", function () {
            assert.throws(() => obj.mostStarredActor(), (Error, 'You have not watched a movie yet this year!'));
        });
        it("Should return mostStarredActor message", function () {
            obj.buyMovie('Rambo',['Sylvester','Stallone']);
            obj.buyMovie('Terminator',['Arnold']);
            obj.buyMovie('Rocky',['Sylvester']);
            obj.watchMovie('Rambo');
            obj.watchMovie('Terminator');
            obj.watchMovie('Terminator');
            obj.watchMovie('Rocky');
            obj.watchMovie('Rocky');
            assert.equal(obj.mostStarredActor(), 'The most starred actor is Sylvester and starred in 2 movies!');
        });         
    });
});