const sidebarDoctor = [
  {
    id: 1,
        title: "Doctor Menu One",
        icon: "fas fa-calendar",
    subNavs: [
      { id: 1, title: "Normal Patient", url: "./page1.html" },
        { id: 2, title: "Serious Patient", url: "./page2.html" },
        { id: 3, title: "Schedules", url: "./page2.html" },
    ],
  },
  {
    id: 2,
      title: "Doctor Menu Two",
    icon: "fas fa-users",
    subNavs: [
      { id: 1, title: "Add Patient", url: "./page3.html" },
      { id: 2, title: "Create Schedule", url: "./page4.html" },
    ],
  },
];

const sidebarPatient = [
    {
        id: 1,
        title: "Patient Menu One",
        icon: "fas fa-calendar",
        subNavs: [
            { id: 1, title: "Take Schedule", url: "./page1.html" },
            { id: 2, title: "Appointment", url: "./page2.html" },
        ],
    },
    {
        id: 2,
        title: "Patient Menu Two",
        icon: "fas fa-users",
        subNavs: [
            { id: 1, title: "Doctors Info", url: "./page3.html" },
            { id: 2, title: "Pathology Info", url: "./page4.html" },
        ],
    },
];
