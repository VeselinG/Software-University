let SkiResort = require('./solution');
const assert = require('chai').assert;
const beforeEach = require('mocha').beforeEach;

describe('SkiResort', function () {
    let obj;
    beforeEach(function () {
        obj = new SkiResort;
    })

    describe("Test constructor", function () {
        it("name => name", function () {
            obj.name='Varna'            
            assert.equal(obj.name, 'Varna');
        });
        it("voters => 0", function () {
            assert.equal(obj.voters, 0);
        });
        it("hotels => []", function () {
            assert.deepEqual(obj.hotels, []);
        });
    });

    describe("Test build()", function () {
        it("Should throw Error if name is blank", function () {                       
            assert.throws(() => obj.build('',1), (Error, 'Invalid input'));
        });        
        it("Should throw Error if beds are less than 1", function () {                       
            assert.throws(() => obj.build('Varna',0), (Error, 'Invalid input'));
        }); 
        it("Should throw Error if beds are less than 1 and name is blank", function () {                       
            assert.throws(() => obj.build('',0), (Error, 'Invalid input'));
        }); 
        it("Test build push in hotels", function () {                       
            obj.build('Varna',2)
            assert.equal(1, obj.hotels.length);
        }); 
        it("Test build push in hotels points", function () {                       
            obj.build('Varna',2)
            assert.equal(0,obj.hotels[0].points)
        }); 
        it("Test build message", function () {                                   
            assert.equal(obj.build('Varna',2),`Successfully built new hotel - Varna`)
        }); 
    });

    describe("Test book()", function () {
        it("Should throw Error if name is blank", function () {                       
            assert.throws(() => obj.book('',1), (Error, 'Invalid input'));
        });        
        it("Should throw Error if beds are less than 1", function () {                       
            assert.throws(() => obj.book('Varna',0), (Error, 'Invalid input'));
        }); 
        it("Should throw Error if beds are less than 1 and name is blank", function () {                       
            assert.throws(() => obj.book('',0), (Error, 'Invalid input'));
        }); 
        it("If there is not such a hotel", function () {                       
            obj.build('Varna',2)
            assert.throws(()=>obj.book('Moskva',5), (Error, `There is no such hotel`));
        });  
        it("If there is no free space in hotel", function () {                       
            obj.build('Varna',2)
            assert.throws(()=>obj.book('Varna',5), (Error, `There is no free space`));
        });
        it("Test beds after booking", function () {                       
            obj.build('Varna',5)
            obj.book('Varna',2)
            assert.equal(3,obj.hotels[0].beds);
        });
        it("Test book() message", function () {                       
            obj.build('Varna',5)            
            assert.equal(obj.book('Varna',2),`Successfully booked`);
        });
    });

    describe("Test leave()", function () {
        it("Should throw Error if name is blank", function () {                       
            assert.throws(() => obj.leave('',1,1), (Error, 'Invalid input'));
        });        
        it("Should throw Error if beds are less than 1", function () {                       
            assert.throws(() => obj.leave('Varna',0,1), (Error, 'Invalid input'));
        }); 
        it("Should throw Error if beds are less than 1 and name is blank", function () {                       
            assert.throws(() => obj.leave('',0,1), (Error, 'Invalid input'));
        });
        it("If there is not such a hotel", function () {                       
            obj.build('Varna',2)
            assert.throws(()=>obj.leave('Moskva',5,5), (Error, `There is no such hotel`));
        });  
        it("Test hotel points after leave()", function () {                       
            obj.build('Varna',5)
            obj.book('Varna',2)
            obj.leave('Varna',2,2)
            assert.equal(4,obj.hotels[0].points);
        });  
        it("Test hotel beds after leave()", function () {                       
            obj.build('Varna',5)
            obj.book('Varna',2)
            obj.leave('Varna',2,2)
            assert.equal(5,obj.hotels[0].beds);
        }); 
        it("Test hotel voters after leave()", function () {                       
            obj.build('Varna',5)
            obj.book('Varna',2)
            obj.leave('Varna',2,2)
            assert.equal(2,obj.voters);
        }); 
        it("Test leave() message", function () {                       
            obj.build('Varna',5)
            obj.book('Varna',2)            
            assert.equal(obj.leave('Varna',2,2),`2 people left Varna hotel`);
        });
    });

    describe("Test averageGrade()", function () {
        it("Should return no voters yet", function () {                       
            assert.equal(obj.averageGrade(), `No votes yet`);
        }); 
        it("Should return averageGrade()", function () {                       
            obj.build('Varna',5)
            obj.book('Varna',2)
            obj.leave('Varna',2,2)
            assert.equal(obj.averageGrade(),'Average grade: 2.00')
        });              
    });

    describe("Test bestHotel()", function () {
        it("Should return no voters yet", function () {                       
            assert.equal(obj.bestHotel, `No votes yet`);
        }); 
        it("Should return bestHotel() correctly", function () {                       
            obj.build('Varna',5)
            obj.book('Varna',2)
            obj.leave('Varna',2,2)
            obj.build('Sofia',5)
            obj.book('Sofia',3)
            obj.leave('Sofia',3,3)

            assert.equal(obj.bestHotel,'Best hotel is Sofia with grade 9. Available beds: 5')
        });              
    });
});
