export let Clamp = (value, min, max) => Math.min(Math.max(value, min), max);

export let CreateOption = (name, value) => {
    let option = document.createElement("option");
    option.innerHTML = name;
    option.value = value;

    return option;
}