class Forum {
    constructor() {
        this._users = [];
        this._questions = [];
        this._id = 1;
    }

    register(username, password, repeatPassword, email) {
        let user = this._users.find(user => user.username === username)
        let emailAdres = this._users.find(user => user.email === email)
        
        if (username === '' || password === '' || repeatPassword === '' || email === '') {
            throw new Error("Input can not be empty")
        } else if (password !== repeatPassword) {
            throw new Error("Passwords do not match")
        } else if (user || emailAdres) {
            throw new Error("This user already exists!")
        } else {
            let objUser = {
                username,
                password,
                email,
                isLogin: false
            };
            this._users.push(objUser)
            return `${username} with ${email} was registered successfully!`
        }
    }

    login(username, password) {
        let user = this._users.find(user => user.username === username)
        if (!user) {
            throw new Error("There is no such user")
        } else if (user && this._users.find(user => user.password === password)) {
            user.isLogin = true;
            return `Hello! You have logged in successfully`
        }
    }

    logout(username, password) {
        let user = this._users.find(user => user.username === username)
        if (!user) {
            throw new Error("There is no such user")
        } else if (user.password===password && user.isLogin===true) {
            user.isLogin = false;
            return `You have logged out successfully`
        }
    }

    postQuestion(username, question) {
        let user = this._users.find(user => user.username === username)
        if (!user || user.isLogin === false) {
            throw new Error("You should be logged in to post questions")
        } else if (question === '') {
            throw new Error("Invalid question")
        } else {
            let obj = {
                username,
                id: this._id,
                question,
                answers: []
            }
            this._questions.push(obj)
            this._id++;
            return "Your question has been posted successfully"
        }
    }

    postAnswer(username, questionId, answer) {
        let user = this._users.find(user => user.username === username)
        let question = this._questions.find(question => question.id === questionId)
        if (!user || user.isLogin === false) {
            throw new Error("You should be logged in to post answers")
        } else if (!answer) {
            throw new Error("Invalid answer")
        } else if (!question) {
            throw new Error("There is no such question")
        } else {
            let obj = {
                username,
                answer
            }
            question.answers.push(obj)
            return "Your answer has been posted successfully"
        }
    }

    showQuestions(){
        let newLine = ''
        return this._questions.reduce((result, question) => {
            result += `${newLine}Question ${question.id} by ${question.username}: ${question.question}`;
            question.answers.forEach(answer => {
                result +=  `\n---${answer.username}: ${answer.answer}`;
            });
            newLine = '\n';
            return result;
        }, '');
    }

}