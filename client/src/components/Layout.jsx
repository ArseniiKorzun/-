const NAV = [
  { key: 'afisha', href: '#/afisha', label: 'Schedule' },
  { key: 'movies', href: '#/movies', label: 'Films' },
  { key: 'admin', href: '#/admin', label: 'Manage sessions' },
]

export default function Layout({ user, path, onLogout, children }) {
  const section = path.split('/')[1]

  return (
    <div className="movies-page">
      <header className="movies-header">
        <div className="header-left">
          <div className="brand">🎬 Films</div>
          <nav className="nav">
            {NAV.map((item) => (
              <a key={item.key} href={item.href} className={section === item.key ? 'active' : undefined}>
                {item.label}
              </a>
            ))}
          </nav>
        </div>
        <div className="user-info">
          <span className="avatar">{user.name?.[0]?.toUpperCase()}</span>
          <span>{user.name}</span>
          <button className="secondary" onClick={onLogout}>Logout</button>
        </div>
      </header>

      {children}
    </div>
  )
}
