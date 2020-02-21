class Computer {

    constructor(ramMemory, cpuGHz, hddMemory){
        this.ramMemory= Number(ramMemory);
        this.cpuGHz=Number(cpuGHz);
        this.hddMemory=Number(hddMemory);
        this.taskManager=[];
        this.installedPrograms=[];
    }

    installAProgram(name,requiredSpace){
        
        if(this.hddMemory-requiredSpace<0){
            throw new Error("There is not enough space on the hard drive");
        }
        else{
            let obj = {
                name,
                requiredSpace
            }
            this.installedPrograms.push(obj)
            this.hddMemory-=requiredSpace

            return obj
        }                
    }

    uninstallAProgram(name){
        let program = this.installedPrograms.find(obj=>obj.name===name);
        if(!program){
            throw new Error("Control panel is not responding")
        }
        else{
            let index = this.installedPrograms.indexOf(program);
            this.installedPrograms.splice(index,1);
            this.hddMemory+=program.requiredSpace;

            return this.installedPrograms;
        }
    }

    openAProgram(name){
        let program = this.installedPrograms.find(obj=>obj.name===name);

        if(!program){
            throw new Error(`The ${name} is not recognized`)
        }

        let isOpen = this.taskManager.find(obj=>obj.name===name);

        if(isOpen){
            throw new Error(`The ${name} is already open`)
        }

        let neededRam = (program.requiredSpace/this.ramMemory)*1.5;
        let neededCPU = ((program.requiredSpace/this.cpuGHz)/500)*1.5;

        let currentRam = this.taskManager.reduce((acc, curr) => acc + curr.ramUsage, 0);
        let currentCPU = this.taskManager.reduce((acc, curr) => acc + curr.cpuUsage, 0);

        if(neededRam+currentRam>=100){
            throw new Error(`${name} caused out of memory exception`)
        }
        if(neededCPU+currentCPU>=100){
            throw new Error(`${name} caused out of cpu exception`)
        }

        let obj = {
            name,
            ramUsage: neededRam,
            cpuUsage: neededCPU
        }

        this.taskManager.push(obj)

        return obj
    }

    taskManagerView(){
        let arr = []
        if(this.taskManager.length===0){
            return "All running smooth so far"
        }
        this.taskManager.forEach(el => {
            arr.push(`Name - ${el.name} | Usage - CPU: ${(el.cpuUsage).toFixed(0)}%, RAM: ${(el.ramUsage).toFixed(0)}%`)
        })
        return arr.join('\n')

    }

}
